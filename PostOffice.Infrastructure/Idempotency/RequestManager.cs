using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PostOffice.Application.Common.Idempotency;
using System;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Idempotency
{
	public class RequestManager : IRequestManager
	{
		private static readonly TimeSpan CacheExpirationPeriod = TimeSpan.FromMinutes(1);

		private readonly IDistributedCache _cache;

		public RequestManager(IDistributedCache cache)
		{
			_cache = cache;
		}


		public async Task<bool> ExistAsync(Guid id)
		{
			var request = await GetAsync<ClientRequest>(id.ToString());

			return request != null;
		}
		public Task SaveRequestAsync<T>(Guid id)
		{
			var request = new ClientRequest()
			{
				Id = id,
				Name = typeof(T).Name,
				Time = DateTime.UtcNow,
			};

			return SetAsync(id.ToString(), request, new DistributedCacheEntryOptions
			{
				SlidingExpiration = CacheExpirationPeriod,
			});
		}
		private async Task<T> GetAsync<T>(string key)
		{
			string valueJson = await _cache.GetStringAsync(key);
			return JsonConvert.DeserializeObject<T>(valueJson);
		}

		private Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
		{
			string valueJson = JsonConvert.SerializeObject(value);
			return _cache.SetStringAsync(key, valueJson, options);
		}
	}
}

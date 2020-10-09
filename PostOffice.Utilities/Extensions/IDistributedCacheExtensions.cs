using Microsoft.Extensions.Caching.Distributed;

namespace PostOffice.Utilities.Extensions
{
	public static class IDistributedCacheExtensions
	{
		public static bool Contains(this IDistributedCache cache, string key)
		{
			return cache.Get(key) != null;
		}
	}
}

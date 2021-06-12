using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PostOffice.Application.Common.Exceptions;
using PostOffice.Application.Common.Locking;
using PostOffice.Utilities.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Idempotency
{
	internal class DistributedLockService : ILockService
	{
		private static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromMilliseconds(300);
		private static readonly TimeSpan DefaultMaximumRetryTime = TimeSpan.FromSeconds(2);
		private static readonly TimeSpan DefaultLockTimeout = TimeSpan.FromSeconds(2);

		private readonly ILogger<DistributedLockService> _logger;

		private readonly IDistributedCache _cache;
		private readonly object _lockObject = new object();

		public DistributedLockService(ILogger<DistributedLockService> logger, IDistributedCache cache)
		{
			_logger = logger;
			_cache = cache;
		}


		public async ValueTask<LockingScope> CreateLockingScope(string key)
		{
			await AcquireLockAsync(key);

			return new LockingScope(this, key);
		}

		public ValueTask AcquireLockAsync(string key, CancellationToken cancellationToken = default)
		{
			return AcquireLockAsync(key, DefaultLockTimeout, cancellationToken);
		}

		public ValueTask AcquireLockAsync(string key, TimeSpan timeout, CancellationToken cancellationToken = default)
		{
			return AcquireLockAsync(key, timeout, DefaultRetryInterval, DefaultMaximumRetryTime);
		}

		private async ValueTask AcquireLockAsync(string key, TimeSpan timeout, TimeSpan retryInterval, TimeSpan maximumRetryTime, CancellationToken cancellationToken = default)
		{
			if (retryInterval == TimeSpan.Zero) retryInterval = DefaultRetryInterval;
			if (maximumRetryTime == TimeSpan.Zero) retryInterval = DefaultMaximumRetryTime;

			// TODO: rewrite with polly
			var maxRetryCount = Math.Max(1, maximumRetryTime.Ticks / retryInterval.Ticks);

			var retryCount = 0;

			while (retryCount++ < maxRetryCount && !cancellationToken.IsCancellationRequested)
			{
				if (TryAcquireLock(key, timeout)) return;

				await Task.Delay(retryInterval, cancellationToken);
			}

			throw new LockCanNotBeAcquiredException($"Lock for key '{key}' could not be acquired!");
		}

		private bool TryAcquireLock(string key, TimeSpan timeout)
		{
			lock (_lockObject)
			{
				if (!_cache.Contains(key))
				{
					_cache.SetString(key, key, new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = timeout,
					});

					_logger?.LogInformation($"Acquired lock for key '{key}'.");

					return true;
				}
			}

			_logger?.LogInformation($"Key '{key}' is locked by another request.");

			return false;
		}

		public ValueTask ReleaseLockAsync(string key, CancellationToken cancellationToken = default)
		{
			lock (_lockObject)
			{
				if (_cache.Contains(key))
				{

					_cache.Remove(key);
					_logger?.LogInformation($"Released lock for key '{key}'");
				}
				else
				{
					_logger?.LogInformation($"There is no lock for key '{key}'");
				}
			}

			return new ValueTask();
		}

	}
}

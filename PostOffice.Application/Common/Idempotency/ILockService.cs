using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Idempotency
{
	public interface ILockService
	{
		ValueTask AcquireLockAsync(string key, CancellationToken cancellationToken = default);
		ValueTask AcquireLockAsync(string key, TimeSpan timeout, CancellationToken cancellationToken = default);
		ValueTask ReleaseLockAsync(string key, CancellationToken cancellationToken = default);
	}
}

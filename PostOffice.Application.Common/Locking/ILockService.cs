using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Locking
{
	public interface ILockService
	{
		ValueTask<LockingScope> CreateLockingScope(string key);

		ValueTask AcquireLockAsync(string key, CancellationToken cancellationToken = default);
		ValueTask AcquireLockAsync(string key, TimeSpan timeout, CancellationToken cancellationToken = default);
		ValueTask ReleaseLockAsync(string key, CancellationToken cancellationToken = default);
	}
}

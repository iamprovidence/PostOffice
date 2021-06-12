using System;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Locking
{
	public class LockingScope : IAsyncDisposable
	{
		private readonly ILockService _lockService;
		private readonly string _key;

		public LockingScope(ILockService lockService, string key)
		{
			_lockService = lockService;
			_key = key;
		}

		public ValueTask DisposeAsync()
		{
			return _lockService.ReleaseLockAsync(_key);
		}
	}
}

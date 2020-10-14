using System;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Idempotency
{
	public interface IRequestManager
	{
		Task<bool> ExistAsync(Guid id);
		Task SaveRequestAsync<T>(Guid id);
	}
}

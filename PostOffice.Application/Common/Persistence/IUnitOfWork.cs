using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Persistence
{
	public interface IUnitOfWork
	{
		public Task BeginTransactionAsync(CancellationToken cancellationToken);
		public Task CommitTransactionAsync(CancellationToken cancellationToken);
		public Task RollbackTransactionAsync(CancellationToken cancellationToken);

	}
}

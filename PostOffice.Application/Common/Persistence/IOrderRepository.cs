using PostOffice.Core.Persistence;
using PostOffice.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Persistence
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task AddAsync(Order order, CancellationToken cancellationToken);
	}
}

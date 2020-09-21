using PostOffice.Application.Common.Persistence;
using PostOffice.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Persistence
{
	public class OrderRepository : IOrderRepository
	{
		public Task AddAsync(Order order, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}

using PostOffice.Application.Orders.ViewModels;
using PostOffice.Core.Persistence;
using PostOffice.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Persistence
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task AddAsync(Order order, CancellationToken cancellationToken);
		Task<bool> DeleteAsync(string ttn, CancellationToken cancellationToken);
		Task<IReadOnlyCollection<OrderListItemViewModel>> FindOrdersAsync(CancellationToken cancellationToken);
	}
}

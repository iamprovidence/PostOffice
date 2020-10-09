using PostOffice.Application.Orders.ViewModels;
using PostOffice.Core.Persistence;
using PostOffice.Domain.Entities;
using PostOffice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Persistence
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order> FindOrderAsync(TTN ttn, CancellationToken cancellationToken);
		Task<IReadOnlyCollection<OrderListItemViewModel>> FindOrdersAsync(CancellationToken cancellationToken);
		Task UpdateAsync(Order order, CancellationToken cancellationToken);
		Task AddAsync(Order order, CancellationToken cancellationToken);
		Task<bool> DeleteAsync(string ttn, CancellationToken cancellationToken);
	}
}

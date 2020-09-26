using MediatR;
using PostOffice.Application.Orders.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class GetOrderListInput : IRequest<IReadOnlyCollection<OrderListItemViewModel>> { }

	public class GetOrderListHandler : IRequestHandler<GetOrderListInput, IReadOnlyCollection<OrderListItemViewModel>>
	{
		public Task<IReadOnlyCollection<OrderListItemViewModel>> Handle(GetOrderListInput request, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}

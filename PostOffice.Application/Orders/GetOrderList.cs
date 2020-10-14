using MediatR;
using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Orders.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class GetOrderListInput : IRequest { }

	public class GetOrderListHandler : IRequestHandler<GetOrderListInput, Unit>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOutputContext<IOrderOutput> _orderOutput;

		public GetOrderListHandler(
			IOrderRepository orderRepository,
			IOutputContext<IOrderOutput> orderOutput
			)
		{
			_orderRepository = orderRepository;
			_orderOutput = orderOutput;
		}

		public async Task<Unit> Handle(GetOrderListInput request, CancellationToken cancellationToken)
		{
			var orders = await _orderRepository.FindOrdersAsync(cancellationToken);

			await _orderOutput.ResponseWith().OrdersLoaded(orders);

			return Unit.Value;
		}
	}
}

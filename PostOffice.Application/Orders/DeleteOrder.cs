using FluentValidation;
using MediatR;
using PostOffice.Application.Common.IntegrationEvents;
using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Orders.Outputs;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class DeleteOrderInput : IRequest
	{
		public string Ttn { get; set; }
	}

	public class DeleteOrderInputValidator : AbstractValidator<DeleteOrderInput>
	{
		public DeleteOrderInputValidator()
		{
			RuleFor(i => i.Ttn)
				.NotEmpty();
		}
	}

	public class DeleteOrder : IRequestHandler<DeleteOrderInput, Unit>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IEventBus _eventBus;
		private readonly IOutputContext<IOrderOutput> _outputContext;

		public DeleteOrder(
			IOrderRepository orderRepository,
			IEventBus eventBus,
			IOutputContext<IOrderOutput> outputContext
			)
		{
			_orderRepository = orderRepository;
			_eventBus = eventBus;
			_outputContext = outputContext;
		}

		public async Task<Unit> Handle(DeleteOrderInput request, CancellationToken cancellationToken)
		{
			var isDeleted = await _orderRepository.DeleteAsync(request.Ttn, cancellationToken);

			if (isDeleted)
			{
				 //_eventBus.Publish(new Events.OrderDeletedIntegrationEvent(request.Ttn));

				await _outputContext.NotifyAll().OrderDeleted(request.Ttn);
			}

			return Unit.Value;
		}
	}
}

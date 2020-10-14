using FluentValidation;
using MediatR;
using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Orders.Interfaces;
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
		private readonly IOutputContext<IOrderOutput> _outputContext;

		public DeleteOrder(
			IOrderRepository orderRepository,
			IOutputContext<IOrderOutput> outputContext
			)
		{
			_orderRepository = orderRepository;
			_outputContext = outputContext;
		}

		public async Task<Unit> Handle(DeleteOrderInput request, CancellationToken cancellationToken)
		{
			var isDeleted = await _orderRepository.DeleteAsync(request.Ttn, cancellationToken);

			if (isDeleted)
			{
				await _outputContext.NotifyAll().OrderDeleted(request.Ttn);
			}

			return Unit.Value;
		}
	}
}

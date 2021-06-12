using FluentValidation;
using MediatR;
using PostOffice.Application.Common.Idempotency;
using PostOffice.Application.Common.Locking;
using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Common.ViewModels;
using PostOffice.Application.Orders.Interfaces;
using PostOffice.Application.Orders.ViewModels;
using PostOffice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class EditOrderLocationInput : IRequest
	{
		public string Ttn { get; set; }
		public LocationViewModel NewLocation { get; set; }
	}

	public class EditOrderLocationInputalidator : AbstractValidator<EditOrderLocationInput>
	{
		public EditOrderLocationInputalidator()
		{
			RuleFor(i => i.Ttn)
				.NotEmpty();

			RuleFor(i => i.NewLocation)
				.NotNull()
				.SetValidator(new LocationViewModelValidator());
		}
	}

	public class LockingBehaviour : IPipelineBehavior<EditOrderLocationInput, Unit>
	{
		private readonly ILockService _lockService;

		public LockingBehaviour(ILockService lockService)
		{
			_lockService = lockService;
		}

		public async Task<Unit> Handle(EditOrderLocationInput request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
		{
			await using (var lockingScope = await _lockService.CreateLockingScope(LockKeyHelper.GetKey(request)))
			{
				return await next();
			}
		}
	}

	public class EditOrderLocation : IRequestHandler<EditOrderLocationInput>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOutputContext<IOrderOutput> _outputContext;

		public EditOrderLocation(
			IOrderRepository orderRepository,
			IOutputContext<IOrderOutput> outputContext
			)
		{
			_orderRepository = orderRepository;
			_outputContext = outputContext;
		}

		public async Task<Unit> Handle(EditOrderLocationInput request, CancellationToken cancellationToken)
		{
			var ttn = new TTN(request.Ttn);
			var order = await _orderRepository.FindOrderAsync(ttn, cancellationToken);

			var newLocation = new Location(request.NewLocation.City, request.NewLocation.Street);
			order.ChangeLocation(newLocation);

			await _orderRepository.UpdateAsync(order, cancellationToken);

			await _outputContext.NotifyAll().OrderLocationChanged(new OrderLocationChangedViewModel
			{
				Ttn = request.Ttn,
				Location = request.NewLocation,
			});

			return Unit.Value;
		}
	}
}

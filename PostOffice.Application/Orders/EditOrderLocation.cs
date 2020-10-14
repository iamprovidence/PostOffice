using FluentValidation;
using MediatR;
using PostOffice.Application.Common.Idempotency;
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

	public class EditOrderLocation : IRequestHandler<EditOrderLocationInput>
	{
		private readonly ILockService _lockService;
		private readonly IOrderRepository _orderRepository;
		private readonly IOutputContext<IOrderOutput> _outputContext;

		public EditOrderLocation(
			ILockService lockService,
			IOrderRepository orderRepository,
			IOutputContext<IOrderOutput> outputContext
			)
		{
			_lockService = lockService;
			_orderRepository = orderRepository;
			_outputContext = outputContext;
		}

		public async Task<Unit> Handle(EditOrderLocationInput request, CancellationToken cancellationToken)
		{
			await _lockService.AcquireLockAsync(LockKeyHelper.GetKey(request), cancellationToken);

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

			await _lockService.ReleaseLockAsync(LockKeyHelper.GetKey(request), cancellationToken);

			return Unit.Value;
		}
	}
}

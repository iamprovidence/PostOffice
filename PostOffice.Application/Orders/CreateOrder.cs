using FluentValidation;
using MediatR;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Common.ViewModels;
using PostOffice.Domain.Entities;
using PostOffice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class CreateOrderInput : IRequest<bool>
	{
		public string Description { get; set; }
		public LocationViewModel SenderLocation { get; set; }
		public LocationViewModel RecipientLocation { get; set; }
		public IReadOnlyCollection<CargoViewModel> Cargos { get; set; }
	}

	public class CreateOrderInputValidator : AbstractValidator<CreateOrderInput>
	{
		public CreateOrderInputValidator()
		{
			RuleFor(i => i.Description)
				.NotEmpty();

			RuleFor(i => i.SenderLocation)
				.NotNull()
				.SetValidator(new LocationViewModelValidator());

			RuleFor(i => i.RecipientLocation)
				.NotNull()
				.SetValidator(new LocationViewModelValidator());

			RuleFor(i => i.Cargos)
				.NotEmpty();

			RuleForEach(i => i.Cargos)
				.SetValidator(new CargoViewModelValidator());
		}
	}

	public class CreateOrder : IRequestHandler<CreateOrderInput, bool>
	{
		private readonly IOrderRepository _orderRepository;

		public CreateOrder(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<bool> Handle(CreateOrderInput request, CancellationToken cancellationToken)
		{
			var senderLocation = new Location(request.SenderLocation.City, request.SenderLocation.City);
			var recipientLocation = new Location(request.RecipientLocation.City, request.RecipientLocation.City);
			var cargos = request.Cargos.Select(c => Cargo.CreateNew(c.Width, c.Length, c.Height));
			var orderToCreate = Order.CreateNew(request.Description, senderLocation, recipientLocation, cargos);

			await _orderRepository.AddAsync(orderToCreate, cancellationToken);

			return true;
		}
	}
}

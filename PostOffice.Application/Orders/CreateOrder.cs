using FluentValidation;
using MediatR;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Common.ViewModels;
using PostOffice.Domain.Entities;
using PostOffice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders
{
	public class CreateOrderInput : IRequest<bool>
	{
		public string Description { get; set; }
		public LocationViewModel SenderLocation { get; set; }
		public LocationViewModel RecipientLocation { get; set; }
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
			var orderToCreate = Order.CreateEmpty(request.Description, senderLocation, recipientLocation);

			await _orderRepository.AddAsync(orderToCreate, cancellationToken);

			return true;
		}
	}
}

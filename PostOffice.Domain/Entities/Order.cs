using PostOffice.Core.Entities;
using PostOffice.Core.Exceptions;
using PostOffice.Domain.Enums;
using PostOffice.Domain.Exceptions;
using PostOffice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace PostOffice.Domain.Entities
{
	public class Order : IEntity<TTN>, IAggregateRoot
	{
		public TTN Identifier { get; }
		public Money Price { get; }
		public string Description { get; }
		public OrderStatus Status { get; private set; }

		public Location SenderLocation { get; }
		public Location RecipientLocation { get; }
		public Location CurrentLocation { get; }

		private readonly ISet<Cargo> _cargos;
		public ICollection<Cargo> Cargos => _cargos;

		protected Order(
			string description,
			Location senderLocation,
			Location recipientLocation,
			IEnumerable<Cargo> cargos = null
			)
		{
			Identifier = TTN.Generate();
			Status = OrderStatus.New;

			_cargos = new HashSet<Cargo>(cargos ?? Enumerable.Empty<Cargo>());

			Description = description;
			SenderLocation = senderLocation;
			RecipientLocation = recipientLocation;

			Price = CalculatePrice();
		}

		private Money CalculatePrice()
		{
			return new Money(amount: _cargos.Count * 10, Currency.UAH);
		}

		public static Order CreateNew(
			string description,
			Location senderLocation,
			Location recipientLocation,
			IEnumerable<Cargo> cargos)
		{
			return new Order(description, senderLocation, recipientLocation, cargos);
		}
		public static Order CreateEmpty(
			string description,
			Location senderLocation,
			Location recipientLocation)
		{
			return new Order(description, senderLocation, recipientLocation);
		}

		public void AddCargo(Cargo cargo)
		{
			if (!_cargos.Add(cargo))
			{
				throw new OrderAlreadyContainsCargoException(Identifier, cargo.Identifier);
			}

			// TODO: AddEvent
		}

		public void RemoveCargo(CargoNumber cargoNumber)
		{
			if (!_cargos.Any(c => c.Identifier == cargoNumber))
			{
				throw new OrderDoesNotContainCargoException(Identifier, cargoNumber);
			}

			// TODO: AddEvent
		}

		public void Cancel()
		{
			if (!CanCancelOrder())
			{
				throw new OrderStatusCannotBeChangedException(Identifier, Status, OrderStatus.Canceled);
			}

			Status = OrderStatus.Canceled;

			// TODO: AddEvent
		}

		private bool CanCancelOrder()
		{
			return Status switch
			{
				OrderStatus.New => true,
				OrderStatus.Delivering => true,
				OrderStatus.Delivered => true,
				OrderStatus.Completed => false,
				OrderStatus.Canceled => false,

				_ => throw new UnexpectedEnumValueException<OrderStatus>(Status),
			};
		}

		public void Complete()
		{
			if (!CanCompleteOrder())
			{
				throw new OrderStatusCannotBeChangedException(Identifier, Status, OrderStatus.Completed);
			}

			Status = OrderStatus.Completed;

			// TODO: AddEvent
		}

		private bool CanCompleteOrder()
		{
			return Status switch
			{
				OrderStatus.New => true,
				OrderStatus.Delivering => true,
				OrderStatus.Delivered => true,
				OrderStatus.Completed => false,
				OrderStatus.Canceled => false,

				_ => throw new UnexpectedEnumValueException<OrderStatus>(Status),
			};
		}
	}
}

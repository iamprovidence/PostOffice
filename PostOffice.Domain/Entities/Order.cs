using PostOffice.Core.Entities;
using PostOffice.Core.Exceptions;
using PostOffice.Domain.Enums;
using PostOffice.Domain.Exceptions;
using PostOffice.Domain.ValueObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PostOffice.Domain.Entities
{
	[Table("Orders")]
	public class Order : IEntity<TTN>, IAggregateRoot
	{
		public TTN Identifier { get; private set; }
		public Money Price { get; private set; }
		public string Description { get; private set; }
		public OrderStatus Status { get; private set; }

		public Location SenderLocation { get; private set; }
		public Location RecipientLocation { get; private set; }
		public Location CurrentLocation { get; private set; }

		private ISet<Cargo> _cargos;
		public ICollection<Cargo> Cargos => _cargos;

		protected Order(
			string description,
			Location senderLocation,
			Location recipientLocation,
			Location currentLocation,
			IEnumerable<Cargo> cargos = null
			)
		{
			Identifier = TTN.Generate();
			Status = OrderStatus.New;

			_cargos = new HashSet<Cargo>(cargos ?? Enumerable.Empty<Cargo>());

			Description = description;
			SenderLocation = senderLocation;
			RecipientLocation = recipientLocation;
			CurrentLocation = currentLocation;

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
			return new Order(description, senderLocation, recipientLocation, senderLocation, cargos);
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


		public void ChangeLocation(Location newLocation)
		{
			CurrentLocation = newLocation;

			if (CurrentLocation != SenderLocation) Status = OrderStatus.Delivering;
			if (CurrentLocation == RecipientLocation) Status = OrderStatus.Delivered;

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

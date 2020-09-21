using PostOffice.Core.Exceptions;
using PostOffice.Domain.ValueObjects;

namespace PostOffice.Domain.Exceptions
{
	public class OrderDoesNotContainCargoException : DomainExceptionBase
	{
		public OrderDoesNotContainCargoException(string message)
			: base(message) { }

		public OrderDoesNotContainCargoException(TTN orderTtn, CargoNumber cargoNumber)
			: base($"Order {orderTtn.Value} does not contain cargo {cargoNumber}") { }
	}
}

using PostOffice.Core.Exceptions;
using PostOffice.Domain.ValueObjects;

namespace PostOffice.Domain.Exceptions
{
	public class OrderAlreadyContainsCargoException : DomainExceptionBase
	{
		public OrderAlreadyContainsCargoException(string message)
			: base(message) { }

		public OrderAlreadyContainsCargoException(TTN orderTtn, CargoNumber cargoNumber)
			: base($"Order {orderTtn.Value} already contains cargo {cargoNumber}") { }
	}
}

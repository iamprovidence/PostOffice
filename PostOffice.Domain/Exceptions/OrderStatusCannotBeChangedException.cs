using PostOffice.Core.Exceptions;
using PostOffice.Domain.Enums;
using PostOffice.Domain.ValueObjects;

namespace PostOffice.Domain.Exceptions
{
	public class OrderStatusCannotBeChangedException : DomainExceptionBase
	{
		public OrderStatusCannotBeChangedException(TTN orderTtn, OrderStatus currentStatus, OrderStatus nextStatus)
			: base($"Cannot change status for order: '{orderTtn.Value}' from {currentStatus} to {nextStatus}'") { }
	}
}

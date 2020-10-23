using PostOffice.Core.Events;
using PostOffice.Domain.Entities;
using PostOffice.Domain.Enums;

namespace PostOffice.Domain.Events
{
	public class OrderStatusChangedDomainEvent : DomainEventBase
	{
		private readonly Order _order;

		public override object AggregateId => _order.Identifier.Value;
		public OrderStatus Status { get; private set; }

		public OrderStatusChangedDomainEvent(Order order, OrderStatus orderStatus)
		{
			_order = order;
			Status = orderStatus;
		}
	}
}

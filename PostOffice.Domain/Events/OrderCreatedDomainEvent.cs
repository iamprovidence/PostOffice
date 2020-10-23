using PostOffice.Core.Events;
using PostOffice.Domain.Entities;

namespace PostOffice.Domain.Events
{
	public class OrderCreatedDomainEvent : DomainEventBase
	{
		private readonly Order _order;

		public override object AggregateId => _order.Identifier.Value;

		public OrderCreatedDomainEvent(Order order)
		{
			_order = order;
		}
	}
}

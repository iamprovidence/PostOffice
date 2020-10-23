using PostOffice.Core.Events;
using PostOffice.Domain.Entities;

namespace PostOffice.Domain.Events
{
	public class OrderCurrentLocationChangedDomainEvent : DomainEventBase
	{
		private readonly Order _order;

		public override object AggregateId => _order.Identifier.Value;
		public string City { get; private set; }
		public string Street { get; private set; }

		public OrderCurrentLocationChangedDomainEvent(Order order, string city, string street)
		{
			_order = order;
			City = city;
			Street = street;
		}
	}
}

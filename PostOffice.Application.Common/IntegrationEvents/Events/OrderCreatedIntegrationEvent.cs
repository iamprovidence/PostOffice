using System;

namespace PostOffice.Application.Common.IntegrationEvents.Events
{
	public class OrderCreatedIntegrationEvent : IIntegrationEvent
	{
		public Guid Id { get; private set; }
		public DateTime CreationDate { get; private set; }
		public EventState State { get; private set; }
		public object AggregateId { get; private set; }
		public string SenderPhoneNumber { get; private set; }

		public OrderCreatedIntegrationEvent(object aggregateId, string senderPhoneNumber)
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.Now;
			State = EventState.NotPublished;
			AggregateId = aggregateId;
			SenderPhoneNumber = senderPhoneNumber;
		}
	}
}

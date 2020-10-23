using System;

namespace PostOffice.Core.Events
{
	public abstract class DomainEventBase : IDomainEvent
	{
		public Guid Id { get; private set; }
		public DateTime CreationDate { get; private set; }
		public abstract object AggregateId { get; }

		public DomainEventBase()
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.Now;
		}
	}
}

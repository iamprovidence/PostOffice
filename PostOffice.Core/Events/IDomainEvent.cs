using System;

namespace PostOffice.Core.Events
{
	public interface IDomainEvent
	{
		Guid Id { get; }
		DateTime CreationDate { get; }
		object AggregateId { get; }
	}
}

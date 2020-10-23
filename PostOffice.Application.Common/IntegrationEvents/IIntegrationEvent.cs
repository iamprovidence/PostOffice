using System;

namespace PostOffice.Application.Common.IntegrationEvents
{
	public interface IIntegrationEvent
	{
		Guid Id { get; }
		DateTime CreationDate { get; }
		EventState State { get; }
		object AggregateId { get; }
	}
}

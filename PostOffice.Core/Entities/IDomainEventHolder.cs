using PostOffice.Core.Events;
using System.Collections.Generic;

namespace PostOffice.Core.Entities
{
	public interface IDomainEventHolder
	{
		IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

		void AddDomainEvent(IDomainEvent domainEvent);

		void ClearDomainEvents();
	}
}

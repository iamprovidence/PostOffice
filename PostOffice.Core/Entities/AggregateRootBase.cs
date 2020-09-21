using PostOffice.Core.Events;
using System.Collections.Generic;

namespace PostOffice.Core.Entities
{
	public abstract class AggregateRootBase : IAggregateRoot
	{
		private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
		public IReadOnlyCollection<IDomainEvent> Events => _events;

		protected void AddEvent(IDomainEvent domainEvent)
		{
			_events.Add(domainEvent);
		}

		public void ClearEvents() => _events.Clear();
	}
}

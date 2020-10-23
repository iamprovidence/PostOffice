using PostOffice.Application.Common.IntegrationEvents;

namespace PostOffice.Infrastructure.IntegrationEvents
{
	// TODO: Add azure queues event bus
	public class AzureQueueEventBus : IEventBus
	{
		public void Publish<T>(T @event) where T : IIntegrationEvent
		{
			throw new System.NotImplementedException();
		}

		public void Subscribe<T, TH>()
			where T : IIntegrationEvent
			where TH : IEventHandler<T>
		{
			throw new System.NotImplementedException();
		}
	}
}

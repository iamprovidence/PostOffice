using PostOffice.Application.Common.IntegrationEvents;

namespace PostOffice.Infrastructure.IntegrationEvents
{
	// TODO: Add redis event bus
	public class RedisEventBus : IEventBus
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

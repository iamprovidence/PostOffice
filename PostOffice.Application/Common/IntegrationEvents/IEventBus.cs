namespace PostOffice.Application.Common.IntegrationEvents
{
	public interface IEventBus
	{
		void Publish<T>(T @event) where T : IIntegrationEvent;

		void Subscribe<T, TH>()
			where T : IIntegrationEvent
			where TH : IEventHandler<T>;
	}
}

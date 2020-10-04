namespace PostOffice.Application.Common.IntegrationEvents
{
	public interface IEventHandler<in TEvent> where TEvent : IIntegrationEvent
	{
		System.Threading.Tasks.ValueTask Handle(TEvent @event);
	}
}

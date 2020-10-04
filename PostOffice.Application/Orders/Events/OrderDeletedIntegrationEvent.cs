using PostOffice.Application.Common.IntegrationEvents;

namespace PostOffice.Application.Orders.Events
{
	public class OrderDeletedIntegrationEvent : IIntegrationEvent
	{
		public string Ttn { get; }

		public OrderDeletedIntegrationEvent(string ttn)
		{
			Ttn = ttn;
		}
	}
}

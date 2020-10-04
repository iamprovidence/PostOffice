using Microsoft.AspNetCore.SignalR;
using PostOffice.API.Hubs;
using PostOffice.Application.Common.IntegrationEvents;
using PostOffice.Application.Orders.Events;
using PostOffice.Application.Orders.Outputs;
using System.Threading.Tasks;

namespace PostOffice.API.ServerSideEvents
{
	public class OrderDeletedEventHandler : IEventHandler<OrderDeletedIntegrationEvent>
	{
		private readonly IHubContext<OrderHub, IOrderOutput> _hubContext;

		public OrderDeletedEventHandler(IHubContext<OrderHub, IOrderOutput> hubContext)
		{
			_hubContext = hubContext;
		}
		public ValueTask Handle(OrderDeletedIntegrationEvent @event)
		{
			_hubContext.Clients.All.OrderDeleted(@event.Ttn);

			return new ValueTask();
		}
	}
}

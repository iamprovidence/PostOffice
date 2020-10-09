using Microsoft.AspNetCore.Authorization;
using PostOffice.API.Configurations;
using PostOffice.Application.Common.Idempotency;
using PostOffice.Application.Orders;
using PostOffice.Application.Orders.Outputs;
using System;
using System.Threading.Tasks;

namespace PostOffice.API.Hubs
{
	[AllowAnonymous]
	[HubConnection(ConnectionType.Order)]
	public class OrderHub : HubBase<IOrderOutput>
	{
		public OrderHub(IServiceProvider serviceProvider)
			: base(serviceProvider) { }

		// wait for response
		public Task<bool> CreateOrder(CreateOrderInput command) => Mediator.Send(command);

		// use output
		public Task GetOrders() => Mediator.Send(new GetOrderListInput());

		// async response
		public async void DeleteOrder(DeleteOrderInput command)
		{
			Mediator.Send(command); // fire and forget
		}

		public async void ChangeOrderLocation(EditOrderLocationInput command)
		{
			Mediator.Send(command);
		}
	}
}

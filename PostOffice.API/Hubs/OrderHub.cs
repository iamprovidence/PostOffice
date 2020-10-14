using Microsoft.AspNetCore.Authorization;
using PostOffice.Application.Common.Network;
using PostOffice.Application.Orders;
using PostOffice.Application.Orders.Interfaces;
using PostOffice.Infrastructure.Network;
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
		public Task<bool> CreateOrder(CreateOrderInput command)
		{
			return Mediator.Send(command);
		}

		// use output
		public Task GetOrders()
		{
			return Mediator.Send(new GetOrderListInput());
		}

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

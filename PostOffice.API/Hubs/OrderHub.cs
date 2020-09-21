using Microsoft.AspNetCore.Authorization;
using PostOffice.API.Configurations;
using PostOffice.Application.Common.Idempotency;
using PostOffice.Application.Common.ViewModels;
using PostOffice.Application.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.API.Hubs
{
	// TODO: move UI Outputs to separate project 
	public interface IOrderHubResponse
	{
		Task OrderCreated(int v);
	}

	[AllowAnonymous]
	[HubConnection(ConnectionType.Order)]
	public class OrderHub : HubBase<IOrderHubResponse>
	{
		protected OrderHub(IServiceProvider serviceProvider)
			: base(serviceProvider) { }

		public Task<IReadOnlyCollection<OrderListItemViewModel>> GetOrders(GetOrderListInput query) => Mediator.Send(query);
		// wait for response
		public Task<bool> CreateOrder(CreateOrderInput command) => Mediator.Send(command);

		// async response
		public async void EditOrder(CreateOrderInput command)
		{
			Mediator.Send(command); // fire and forget

			await Task.Delay(2000);
		}
	}
}

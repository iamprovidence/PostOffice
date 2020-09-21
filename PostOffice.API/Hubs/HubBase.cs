using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.API.Configurations;
using PostOffice.Application.Common.Idempotency;
using PostOffice.Application.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.API.Hubs
{
	[HubConnection(ConnectionType.Notification)]
	public abstract class HubBase<TClientHub>: Hub<TClientHub>
		where TClientHub : class
	{
		private readonly IServiceProvider _serviceProvider;

		private readonly Lazy<IMediator> _mediator;
		private readonly Lazy<IConnectionManager> _connectionManager;
		private readonly Lazy<IReadOnlyUserContext> _userContext;

		protected HubBase(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;

			_mediator = new Lazy<IMediator>(InitService<IMediator>);
			_connectionManager = new Lazy<IConnectionManager>(InitService<IConnectionManager>);
			_userContext = new Lazy<IReadOnlyUserContext>(InitService<IReadOnlyUserContext>);
		}

		private TService InitService<TService>()
		{
			return _serviceProvider.GetService<TService>();
		}

		public IMediator Mediator => _mediator.Value;
		public IConnectionManager ConnectionManager => _connectionManager.Value;
		public IReadOnlyUserContext UserContext => _userContext.Value;

		public override Task OnConnectedAsync()
		{
			var types = GetHubConnectionTypes();
			var userId = UserContext.UserIdentifier;
			var connectionId = Context.ConnectionId;

			foreach (var connectionType in types)
			{
				ConnectionManager.AddConnection(connectionType, userId, connectionId);
			}

			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			ConnectionManager.RemoveConnection(Context.ConnectionId);

			return base.OnDisconnectedAsync(exception);
		}

		private IReadOnlyCollection<ConnectionType> GetHubConnectionTypes()
		{
			return GetType()
				.GetCustomAttributes(typeof(ConnectionType), inherit: true)
				.Cast<ConnectionType>()
				.ToArray();
		}
	}
}

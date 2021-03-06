using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Identity;
using PostOffice.Application.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Network
{
	[HubConnection(ConnectionType.Notification)]
	public abstract class HubBase<TClientHub>: Hub<TClientHub>
		where TClientHub : class
	{
		private readonly IServiceProvider _serviceProvider;

		private readonly Lazy<IMediator> _mediator;
		private readonly Lazy<IConnectionManager> _connectionManager;
		private readonly Lazy<IReadOnlyUserContext> _userContext;

		public HubBase(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;

			InitializeRequest();

			_mediator = new Lazy<IMediator>(InitService<IMediator>);
			_connectionManager = new Lazy<IConnectionManager>(InitService<IConnectionManager>);
			_userContext = new Lazy<IReadOnlyUserContext>(InitService<IReadOnlyUserContext>);
		}

		private TService InitService<TService>()
		{
			return _serviceProvider.GetRequiredService<TService>();
		}

		private void InitializeRequest()
		{
			InitService<IRequestContextInitializer>().SetCallerType(GetType());
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
				.GetCustomAttributes(typeof(HubConnectionAttribute), inherit: false)
				.Cast<HubConnectionAttribute>()
				.Select(a => a.ConnectionType)
				.ToArray();
		}
	}
}

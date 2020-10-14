using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Network;
using PostOffice.Application.Common.OutputPort;

namespace PostOffice.Infrastructure.Network
{
	internal class SignalROutputContext<TOutputPort> : IOutputContext<TOutputPort>
		where TOutputPort : class, IOutputPort
	{
		private readonly IRequestContextAccessor _requestContext;
		private readonly IConnectionManager _connectionManager;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public SignalROutputContext(
			IRequestContextAccessor requestContext,
			IConnectionManager connectionManager,
			IServiceScopeFactory serviceScopeFactory
			)
		{
			_requestContext = requestContext;
			_connectionManager = connectionManager;
			_serviceScopeFactory = serviceScopeFactory;
		}

		public TOutputPort NotifyAll()
		{
			return GetHubClients().All;
		}

		public TOutputPort ResponseWith()
		{
			var userConnections = _connectionManager.GetConnections("Default");

			return GetHubClients().Clients(userConnections);
		}

		private object BuildHubContext()
		{
			var outputPortType = typeof(TOutputPort);
			var hubType = _requestContext.CallerType;
			// typeof IHubContext<Hub<TOutputPort>, TOutputPort>
			var hubContextType = typeof(IHubContext<,>).MakeGenericType(hubType, outputPortType);

			using var serviceScope = _serviceScopeFactory.CreateScope();
			return serviceScope.ServiceProvider.GetRequiredService(hubContextType);
		}

		private IHubClients<TOutputPort> GetHubClients()
		{
			var hubContext = BuildHubContext();
			var clientPropertyInfo = hubContext.GetType().GetProperty(nameof(IHubContext<Hub<TOutputPort>, TOutputPort>.Clients));

			return clientPropertyInfo.GetValue(hubContext) as IHubClients<TOutputPort>;
		}
	}
}

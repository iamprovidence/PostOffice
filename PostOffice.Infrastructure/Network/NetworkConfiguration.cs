using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Network;
using PostOffice.Application.Common.OutputPort;

namespace PostOffice.Infrastructure.Network
{
	public static class NetworkConfiguration
	{
		public static void AddNetworkConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSignalR();

			services.AddSingleton<IConnectionManager, ConnectionManager>();

			services.AddScoped<SignalRRequestContext>();
			services.AddScoped<IRequestContextAccessor, SignalRRequestContext>(sp => sp.GetRequiredService<SignalRRequestContext>());
			services.AddScoped<IRequestContextInitializer, SignalRRequestContext>(sp => sp.GetRequiredService<SignalRRequestContext>());

			services.AddScoped(typeof(IOutputContext<>), typeof(SignalROutputContext<>));
		}
	}
}

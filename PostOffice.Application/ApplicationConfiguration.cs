using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Behaviours;
using PostOffice.Application.Orders;

namespace PostOffice.Application
{
	public static class ApplicationConfiguration
	{
		public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(typeof(ApplicationConfiguration).Assembly);

			// register before to lock whole transaction
			services.AddTransient<IPipelineBehavior<EditOrderLocationInput, Unit>, LockingBehaviour>();

			services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
		}
	}
}

using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostOffice.Application.Common.Behaviours
{
	public static class ApplicationConfiguration
	{
		public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(typeof(ApplicationConfiguration).Assembly);

			services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
		}
	}
}

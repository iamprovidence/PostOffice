using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using PostOffice.SmsSender.Application.Services;
using System;

[assembly: FunctionsStartup(typeof(PostOffice.SmsSender.Startup))]

namespace PostOffice.SmsSender
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var services = builder.Services;
			var configuration = BuildConfiguration(builder.Services);

			services.AddScoped<SmsSenderAppService>();
			services.AddApplicationInsightsTelemetry();
		}

		public static IConfiguration BuildConfiguration(IServiceCollection services)
		{
			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();


			var config = configurationBuilder.Build();

			// Because functions doesn't let us configure configurations pre-injection we have to place the injected instace of IConfiguration
			services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), config));

			services.AddSingleton<ILoggerFactory, LoggerFactory>();
			services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

			return config;
		}
	}
}

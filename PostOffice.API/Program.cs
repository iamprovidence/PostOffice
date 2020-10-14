using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PostOffice.Infrastructure.Configurations;

namespace PostOffice.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost
				.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddEnvironmentVariables();
					config.AddLocalEnvironmentVariablesConfiguration();
				})
				.UseStartup<Startup>()
				.UseDefaultServiceProvider(configure =>
				{
					configure.ValidateOnBuild = true;
					configure.ValidateScopes = true;
				});
	}
}

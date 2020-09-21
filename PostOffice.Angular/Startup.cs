using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Angular.Configurations;
using System;

namespace PostOffice.Angular
{
	public class Startup : IStartup
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_environment = environment;
			_configuration = configuration;
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(_configuration);
			services.AddAngularSpa(_configuration);

			return services.BuildServiceProvider();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc(_environment);
			app.UseAngularSpa(_environment);
		}
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.API.Hubs;
using PostOffice.Application;
using PostOffice.Infrastructure.HealthChecks;
using PostOffice.Infrastructure.Idempotency;
using PostOffice.Infrastructure.Identity;
using PostOffice.Infrastructure.Network;
using PostOffice.Infrastructure.Persistence;
using System;

namespace PostOffice.API
{
	public class Startup : IStartup
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_configuration = configuration;
			_environment = environment;
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// TODO: use autofac here
			services.AddCors();
			services.AddNetworkConfiguration(_configuration);
			services.AddIdempotencyServices(_configuration, option =>
			{
				option.InstanceName = _configuration.GetValue<string>("Redis:InstanceName");
				option.ConnectionString = _configuration.GetValue<string>("Redis:ConnectionString");
			});
			services.AddApplicationServices(_configuration);
			services.AddIdentity(_configuration);
			services.AddPersistenceStorage(_configuration, option =>
			{
				option.ConnectionString = _configuration.GetValue<string>("MongoDb:ConnectionString");
				option.DatabaseName = _configuration.GetValue<string>("MongoDb:DatabaseName");
			});
			services.AddHealthChecks(_configuration);

			return services.BuildServiceProvider(validateScopes: true);
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseCors(configuration =>
			{
				configuration
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin();
			});

			app.UseRouting();
			app.UseIdentity(_environment);

			app.UseHealthChecks(_environment);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<OrderHub>("/orders");
			});
		}
	}
}

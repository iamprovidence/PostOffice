using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.API.Configurations;
using PostOffice.API.Hubs;
using StackExchange.Redis;
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
			services.AddCors();
			services.AddSignalR();
			services.AddMediatR(typeof(Application.Common.Identity.IUserContext).Assembly);

			var redisConnectionString = _configuration.GetValue<string>("Redis:ConnectionString");
			//services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
			services.AddDistributedMemoryCache();
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = redisConnectionString;
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

			app.UseHealthChecks(_environment);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<OrderHub>("/orders");
			});
		}
	}
}

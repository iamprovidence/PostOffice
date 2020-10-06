using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.API.Configurations;
using PostOffice.API.Hubs;
using PostOffice.API.ServerSideEvents;
using PostOffice.Application.Common.Idempotency;
using PostOffice.Application.Common.IntegrationEvents;
using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Orders.Events;
using PostOffice.Infrastructure.HealthChecks;
using PostOffice.Infrastructure.Idempotency;
using PostOffice.Infrastructure.IntegrationEvents;
using PostOffice.Infrastructure.OutputPort;
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
			// TODO: regrooup services
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

			// TODO: move to separate class
			services.Configure<MongoDbContextConfiguration>(option =>
			{
				// TODO: get from config file
				option.ConnectionString = "mongodb://192.168.99.100";
				option.DatabaseName = "PostOffice";
			});

			services.AddUserContext(_configuration);

			services.AddScoped<MongoContext>();
			services.AddScoped<IOrderRepository, OrderRepository>();

			services.AddSingleton<IConnectionManager, ConnectionManager>();

			services.AddScoped<SignalRRequestContext>();
			services.AddScoped<IRequestContextAccessor, SignalRRequestContext>(sp => sp.GetRequiredService<SignalRRequestContext>());
			services.AddScoped<IRequestContextInitializer, SignalRRequestContext>(sp => sp.GetRequiredService<SignalRRequestContext>());

			// TODO: move to separate class
			services.AddScoped(typeof(IOutputContext<>), typeof(SignalROutputContext<>));

			services.AddSingleton<IEventBus, InMemoryEventBus>();
			services.AddScoped<OrderDeletedEventHandler>();


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

			IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

			eventBus.Subscribe<OrderDeletedIntegrationEvent, OrderDeletedEventHandler>();

			app.UseUserContext(_environment);
			app.UseHealthChecks(_environment);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<OrderHub>("/orders");
			});
		}
	}
}

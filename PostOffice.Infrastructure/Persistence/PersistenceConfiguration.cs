using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Orders.Interfaces;
using PostOffice.Infrastructure.Persistence.Repositories;
using System;

namespace PostOffice.Infrastructure.Persistence
{
	public static class PersistenceConfiguration
	{
		public static void AddPersistenceStorage(this IServiceCollection services, IConfiguration configuration, Action<MongoDbConfiguration> configureOptions)
		{
			services.Configure<MongoDbConfiguration>(configureOptions);

			services.AddScoped<MongoContext>();
			services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MongoContext>());

			services.AddScoped<IOrderRepository, OrderRepository>();
		}
	}
}

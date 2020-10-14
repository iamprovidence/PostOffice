using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Idempotency;
using StackExchange.Redis;
using System;

namespace PostOffice.Infrastructure.Idempotency
{
	public static class IdempotencyConfiguration
	{
		public static void AddIdempotencyServices(this IServiceCollection services, IConfiguration configuration, Action<RedisConfiguration> configureOptions)
		{
			var redisConfiguration = new RedisConfiguration();
			configureOptions(redisConfiguration);

			services.AddDistributedMemoryCache();
			services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));

			services.AddStackExchangeRedisCache(options =>
			{
				options.InstanceName = redisConfiguration.InstanceName;
				options.Configuration = redisConfiguration.ConnectionString;
			});

			services.AddScoped<ILockService, DistributedLockService>();
		}
	}
}

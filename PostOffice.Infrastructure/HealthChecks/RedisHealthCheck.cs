using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.HealthChecks
{
	internal class RedisHealthCheck : IHealthCheck
	{
		private readonly IConnectionMultiplexer _connectionMultiplexer;

		public RedisHealthCheck(IConnectionMultiplexer connectionMultiplexer)
		{
			_connectionMultiplexer = connectionMultiplexer;
		}

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			try
			{
				_connectionMultiplexer.GetDatabase();

				return Task.FromResult(HealthCheckResult.Healthy());
			}
			catch (System.Exception ex)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
			}
		}
	}
}

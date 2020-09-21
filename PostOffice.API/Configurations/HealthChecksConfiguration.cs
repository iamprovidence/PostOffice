using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PostOffice.Core.HealthChecks;
using PostOffice.Infrastructure.Configuration;
using System.Linq;

namespace PostOffice.API.Configurations
{
	internal static class HealthChecksConfiguration
	{
		public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddHealthChecks()
				.AddMongoDb(configuration.GetValue<string>("MongoDb:ConnectionString"));
				//.AddCheck<RedisHealthCheck>(name: "Redis");
		}

		public static void UseHealthChecks(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = (context, report) =>
				{
					context.Response.ContentType = "application/json";

					var response = new HealthCheckResponse
					{
						Status = report.Status,
						Duration = report.TotalDuration,
						HealthChecks = report.Entries.Select(e => new HealthCheck
						{
							ComponentName = e.Key,
							Status = e.Value.Status,
							Description = e.Value.Description,
						}),
					};

					var responseJson = JsonConvert.SerializeObject(response, Formatting.Indented);
					return context.Response.WriteAsync(responseJson);
				}
			});

		}
	}
}

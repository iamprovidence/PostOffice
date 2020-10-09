using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using PostOffice.Infrastructure.HealthChecks.Models;
using System.Linq;

namespace PostOffice.Infrastructure.HealthChecks
{
	public static class HealthChecksConfiguration
	{
		public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddHealthChecks()
				.AddCheck<RedisHealthCheck>(name: "Redis")
				.AddMongoDb(
					mongodbConnectionString: configuration.GetValue<string>("MongoDb:ConnectionString"),
					name: "Mongo",
					failureStatus: HealthStatus.Unhealthy);

			services
				.AddHealthChecksUI(opt =>
				{
					opt.SetEvaluationTimeInSeconds(5);
					opt.MaximumHistoryEntriesPerEndpoint(10);
					opt.SetApiMaxActiveRequests(1);

					opt.AddHealthCheckEndpoint("PostOffice health", "/health-ui");
				})
				.AddInMemoryStorage();
		}

		public static void UseHealthChecks(this IApplicationBuilder app, IWebHostEnvironment environment)
		{

			app.UseHealthChecks("/health-ui", new HealthCheckOptions
			{
				Predicate = (healthCheckRegistration) => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
			});

			app.UseHealthChecks("/health-json", new HealthCheckOptions
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

			app.UseHealthChecksUI(options =>
			{
				options.UIPath = "/health";
				options.ApiPath = "/health-ui-api";
				options.UseRelativeApiPath = false;
				options.UseRelativeResourcesPath = false;
			});
		}
	}
}

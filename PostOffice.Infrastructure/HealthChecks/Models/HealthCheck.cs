using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PostOffice.Infrastructure.HealthChecks.Models
{
	internal class HealthCheck
	{

		[JsonConverter(typeof(StringEnumConverter))]
		public HealthStatus Status { get; set; }
		public string ComponentName { get; set; }
		public string Description { get; set; }
	}
}

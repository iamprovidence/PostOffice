using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PostOffice.Core.HealthChecks
{
	public class HealthCheck
	{

		[JsonConverter(typeof(StringEnumConverter))]
		public HealthStatus Status { get; set; }
		public string ComponentName { get; set; }
		public string Description { get; set; }
	}
}

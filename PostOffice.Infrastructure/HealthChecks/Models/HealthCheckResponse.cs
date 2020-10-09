using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace PostOffice.Infrastructure.HealthChecks.Models
{
	internal class HealthCheckResponse
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public HealthStatus Status { get; set; }
		public TimeSpan Duration { get; set; }
		public IEnumerable<HealthCheck> HealthChecks { get; set; }
	}
}

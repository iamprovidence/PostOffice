using Microsoft.Extensions.Configuration;

namespace PostOffice.Infrastructure.Configurations
{
	public static class ConfigurationExtensions
	{
		public static IConfigurationBuilder AddLocalEnvironmentVariablesConfiguration(this IConfigurationBuilder builder)
		{
			return builder.Add(new LocalEnvironmentVariablesSource());
		}
	}
}

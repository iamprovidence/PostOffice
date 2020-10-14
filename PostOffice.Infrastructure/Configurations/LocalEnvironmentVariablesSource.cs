using Microsoft.Extensions.Configuration;

namespace PostOffice.Infrastructure.Configurations
{
	public class LocalEnvironmentVariablesSource : IConfigurationSource
	{
		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new LocalEnvironmentVariablesConfigurationProvider();
		}
	}
}

using Microsoft.Extensions.Options;
using PostOffice.Angular.Application.ViewModels;

namespace PostOffice.Angular.Application.Services
{
	public class SettingsAppService
	{
		private readonly EnvironmentUrls _environmentUrls;

		public SettingsAppService(IOptions<EnvironmentUrls> options)
		{
			_environmentUrls = options.Value;
		}

		public EnvironmentData GetEnvironmentData()
		{
			return new EnvironmentData
			{
				EnvironmentUrls = _environmentUrls,
			};
		}
	}
}

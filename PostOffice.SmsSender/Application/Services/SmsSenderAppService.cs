using Microsoft.Extensions.Logging;
using PostOffice.SmsSender.Application.Models;

namespace PostOffice.SmsSender.Application.Services
{
	public class SmsSenderAppService
	{
		private readonly ILogger<SmsSenderAppService> _logger;

		public SmsSenderAppService(ILogger<SmsSenderAppService> logger)
		{
			_logger = logger;
		}

		public void SendSms(SendSmsModel sendSmsModel)
		{
			_logger.LogInformation("Sms has been successfully sent");
		}
	}
}


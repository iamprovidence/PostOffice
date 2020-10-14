using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PostOffice.SmsSender.Application.Models;
using PostOffice.SmsSender.Application.Services;
using System.ComponentModel;

namespace PostOffice.SmsSender
{
	public class SendSmsMessage
	{
		private readonly SmsSenderAppService _smsSenderAppService;
		private readonly ILogger<SendSmsMessage> _logger;

		public SendSmsMessage(SmsSenderAppService smsSenderAppService, ILogger<SendSmsMessage> logger)
		{
			_smsSenderAppService = smsSenderAppService;
			_logger = logger;
		}

		[FunctionName("SendSms")]
		public void Run([QueueTrigger("send-sms", Connection = "SmsSenderStorage")] SendSmsModel sendSmsModel)
		{
			_logger.LogInformation($"[[Queue trigger]] SendSms with params: Phone: {sendSmsModel.PhoneNumber}, Message: {sendSmsModel.Message}");

			_smsSenderAppService.SendSms(sendSmsModel);
		}


		[FunctionName("TestSendSms")]
		[Description("http://localhost:7071/api/TestSendSms?phoneNumber=1111&message=Hello")]
		[return: Queue("send-sms", Connection = "SmsSenderStorage")]
		public SendSmsModel RunTest([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] SendSmsModel sendSmsModel)
		{
			_logger.LogInformation("[[HTTP trigger]] TestSendSms");

			return sendSmsModel;
		}
	}
}

using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using PostOffice.Application.Common.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Behaviours
{
	public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
	{
		private readonly ILogger _logger;
		private readonly IReadOnlyUserContext _userContext;

		public LoggingBehaviour(ILogger<TRequest> logger, IReadOnlyUserContext userContext)
		{
			_logger = logger;
			_userContext = userContext;
		}

		public async Task Process(TRequest request, CancellationToken cancellationToken)
		{
			var requestName = typeof(TRequest).Name;
			var userId = _userContext.UserIdentifier;

			_logger.LogInformation($"Request: {requestName} {userId} {request}");
		}
	}
}

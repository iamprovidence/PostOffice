using MediatR;
using Microsoft.Extensions.Logging;
using PostOffice.Application.Common.Identity;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Behaviours
{
	public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly Stopwatch _timer;
		private readonly ILogger<TRequest> _logger;
		private readonly IReadOnlyUserContext _userContext;

		public PerformanceBehaviour(ILogger<TRequest> logger, IReadOnlyUserContext userContext)
		{
			_timer = new Stopwatch();

			_logger = logger;
			_userContext = userContext;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			_timer.Start();

			var response = await next();

			_timer.Stop();

			var elapsedMilliseconds = _timer.ElapsedMilliseconds;

			if (elapsedMilliseconds > 500)
			{
				var requestName = typeof(TRequest).Name;
				var userId = _userContext.UserIdentifier;

				_logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
					requestName, elapsedMilliseconds, userId, request);
			}

			return response;
		}
	}
}

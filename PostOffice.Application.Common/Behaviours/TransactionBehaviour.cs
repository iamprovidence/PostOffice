using MediatR;
using Microsoft.Extensions.Logging;
using PostOffice.Application.Common.Identity;
using PostOffice.Application.Common.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Behaviours
{
	public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<TRequest> _logger;
		private readonly IReadOnlyUserContext _userContext;
		private readonly IUnitOfWork _unitOfWork;

		public TransactionBehaviour(ILogger<TRequest> logger, IReadOnlyUserContext userContext, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_userContext = userContext;
			_unitOfWork = unitOfWork;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			await _unitOfWork.BeginTransactionAsync(cancellationToken);
			var response = await next();

			try
			{
				await _unitOfWork.CommitTransactionAsync(cancellationToken);
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync(cancellationToken);
				_logger.LogError($"Transaction failed for [{_userContext.UserIdentifier}]");
			}

			return response;
		}
	}
}

using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Auth
{
	public abstract class RequestAuthorizationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
		where TRequest : notnull
	{
		private readonly HashSet<IAuthorizationRequirement> _requirements;
		private readonly ISender _sender;

		public RequestAuthorizationPreProcessor(ISender sender)
		{
			_requirements = new HashSet<IAuthorizationRequirement>();
			_sender = sender;
		}

		protected abstract void RegisterRequirements(TRequest request);

		protected void AddRequirement(IAuthorizationRequirement requirement)
		{
			_requirements.Add(requirement);
		}

		public async Task Process(TRequest request, CancellationToken cancellationToken)
		{
			foreach (var requirement in _requirements.Distinct())
			{
				var result = await _sender.Send(requirement, cancellationToken);

				if (!result.IsAuthorized)
				{
					throw new UnauthorizedAccessException(result.FailureMessage);
				}
			}
		}
	}
}

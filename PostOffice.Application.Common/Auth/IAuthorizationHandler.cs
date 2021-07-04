using MediatR;

namespace PostOffice.Application.Common.Auth
{
	public interface IAuthorizationHandler<T> : IRequestHandler<T, AuthorizationResult>
		where T : IAuthorizationRequirement
	{
	}
}

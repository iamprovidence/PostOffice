using MediatR;

namespace PostOffice.Application.Common.Auth
{
	public interface IAuthorizationRequirement : IRequest<AuthorizationResult> { }
}

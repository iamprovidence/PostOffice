using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Application.Common.Auth.Concrete
{
	public class DeleteOrderRequirement : IAuthorizationRequirement
	{
		public string UserId { get; set; }
		public string OrderTtn { get; set; }
	}
	class DeleteOrderRequirementHandler : IAuthorizationHandler<DeleteOrderRequirement>
	{
		private readonly IApplicationDbContext _applicationDbContext;

		public DeleteOrderRequirementHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<AuthorizationResult> Handle(DeleteOrderRequirement request, CancellationToken cancellationToken)
		{
			var userId = request.UserId;
			var userCourseSubscription = null; // logic

			if (userCourseSubscription != null)
				return AuthorizationResult.Succeed();

			return AuthorizationResult.Fail("You don't have a subscription to this course.");
		}
	}
}

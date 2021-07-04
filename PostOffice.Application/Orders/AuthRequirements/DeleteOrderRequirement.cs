using PostOffice.Application.Orders.Interfaces;
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
		private readonly IOrderRepository _orderRepository;

		public DeleteOrderRequirementHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<AuthorizationResult> Handle(DeleteOrderRequirement request, CancellationToken cancellationToken)
		{
			var userId = request.UserId;

			// logic
			var canDeleteOrder = false;

			if (canDeleteOrder != null)
			{
				return AuthorizationResult.Succeed();
			}

			return AuthorizationResult.Fail("You don't have a subscription to this course.");
		}
	}
}

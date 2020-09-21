using PostOffice.Application.Common.Identity;

namespace PostOffice.Infrastructure.Identity
{
	class UserContextSettable : IUserContextSettable
	{
		private readonly IUserContext _userContext;

		public UserContextSettable(IUserContext userContext)
		{
			_userContext = userContext;
		}

		public void SetContext(string userIdentifier)
		{
			_userContext.UserIdentifier = userIdentifier;
		}
	}
}

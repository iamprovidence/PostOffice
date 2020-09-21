using PostOffice.Application.Common.Identity;

namespace PostOffice.Infrastructure.Identity
{
	public class UserContext : IUserContext
	{
		public string UserIdentifier { get; set; }
	}
}

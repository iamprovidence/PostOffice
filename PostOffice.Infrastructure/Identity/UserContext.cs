using PostOffice.Application.Common.Identity;

namespace PostOffice.Infrastructure.Identity
{
	public class UserContext : IUserContext
	{
		// TODO: fix this does not get initialized
		public string UserIdentifier { get; set; } = "Default";
	}
}

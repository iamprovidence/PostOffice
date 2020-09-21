namespace PostOffice.Application.Common.Identity
{
	public interface IUserContext : IReadOnlyUserContext
	{
		new string UserIdentifier { get; set; }
	}
}

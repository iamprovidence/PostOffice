namespace PostOffice.Application.Common.Identity
{
	public interface IReadOnlyUserContext
	{
		string UserIdentifier { get; }
	}
}

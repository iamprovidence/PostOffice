namespace PostOffice.Application.Common.Identity
{
	public interface IUserContextSettable
	{
		void SetContext(string userIdentifier);
	}
}

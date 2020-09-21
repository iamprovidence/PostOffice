using PostOffice.Core.Entities;

namespace PostOffice.Core.Persistence
{
	public interface IRepository<T>
		where T : IAggregateRoot
	{
	}
}

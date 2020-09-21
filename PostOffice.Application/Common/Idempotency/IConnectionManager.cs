using System.Collections.Generic;

namespace PostOffice.Application.Common.Idempotency
{
	public interface IConnectionManager
	{
		void AddConnection(ConnectionType type, string userId, string connectionId);
		void RemoveConnection(string connectionId);

		IReadOnlyList<string> GetConnections(string userId);
	}
}

using PostOffice.Application.Common.Idempotency;
using System.Collections.Generic;
using System.Linq;

namespace PostOffice.Infrastructure.Idempotency
{
	// TODO: rewrite this simple implementation
	// TODO: make thread safe
	public class ConnectionManager : IConnectionManager
	{
		class ConnectionEntry
		{
			public ConnectionType Type { get; set; }
			public string UserId { get; set; }
			public string ConnectionId { get; set; }
		}

		private Dictionary<string, List<ConnectionEntry>> _connections = new Dictionary<string, List<ConnectionEntry>>();

		public void AddConnection(ConnectionType type, string userId, string connectionId)
		{
			if (!_connections.ContainsKey(userId))
			{
				_connections[userId] = new List<ConnectionEntry>();
			}

			_connections[userId].Add(new ConnectionEntry
			{
				ConnectionId = connectionId,
				Type = type,
				UserId = userId,
			});
		}

		public IReadOnlyList<string> GetConnections(string userId)
		{
			return _connections[userId].Select(c => c.ConnectionId).ToArray();
		}

		public void RemoveConnection(string connectionId)
		{
			foreach (string userId in _connections.Keys)
			{
				var entry = _connections[userId].SingleOrDefault(c => c.ConnectionId == connectionId);
				bool isRemoved = _connections[userId].Remove(entry);
				if (isRemoved) break;
			}
		}
	}
}

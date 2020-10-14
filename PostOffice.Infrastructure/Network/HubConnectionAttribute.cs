using PostOffice.Application.Common.Network;
using System;

namespace PostOffice.Infrastructure.Network
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class HubConnectionAttribute : Attribute
	{
		public ConnectionType ConnectionType { get; }

		public HubConnectionAttribute(ConnectionType connectionType)
		{
			ConnectionType = connectionType;
		}
	}
}

using PostOffice.Application.Common.Idempotency;
using System;

namespace PostOffice.API.Configurations
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

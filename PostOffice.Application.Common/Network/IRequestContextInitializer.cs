using System;

namespace PostOffice.Application.Common.Network
{
	public interface IRequestContextInitializer
	{
		public void SetCallerType(Type callerType);
	}
}

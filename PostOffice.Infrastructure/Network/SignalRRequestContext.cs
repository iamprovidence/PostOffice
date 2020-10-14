using PostOffice.Application.Common.Network;
using System;

namespace PostOffice.Infrastructure.Network
{
	internal class SignalRRequestContext : IRequestContextAccessor, IRequestContextInitializer
	{
		public Type CallerType { get; private set; }

		public void SetCallerType(Type callerType)
		{
			CallerType = callerType;
		}
	}
}

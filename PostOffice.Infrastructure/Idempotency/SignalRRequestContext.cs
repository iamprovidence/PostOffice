using PostOffice.Application.Common.Idempotency;
using System;

namespace PostOffice.Infrastructure.Idempotency
{
	public class SignalRRequestContext : IRequestContextAccessor, IRequestContextInitializer
	{
		private Type _callerType;

		public Type CallerType => _callerType;

		public void SetCallerType(Type callerType)
		{
			_callerType = callerType;
		}
	}
}

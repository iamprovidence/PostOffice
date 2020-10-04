using System;

namespace PostOffice.Application.Common.Idempotency
{
	public interface IRequestContextInitializer
	{
		public void SetCallerType(Type callerType);
	}
}

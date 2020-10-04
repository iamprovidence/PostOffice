using System;

namespace PostOffice.Application.Common.Idempotency
{
	public interface IRequestContextAccessor
	{
		public Type CallerType { get; }
	}
}

using System;

namespace PostOffice.Application.Common.Network
{
	public interface IRequestContextAccessor
	{
		public Type CallerType { get; }
	}
}

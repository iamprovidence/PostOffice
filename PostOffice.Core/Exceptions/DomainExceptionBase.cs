using System;

namespace PostOffice.Core.Exceptions
{
	public abstract class DomainExceptionBase : Exception
	{
		public DomainExceptionBase(string message)
			: base(message) { }
	}
}

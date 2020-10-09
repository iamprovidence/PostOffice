using System;

namespace PostOffice.Core.Exceptions
{
	public abstract class ApplicationExceptionBase : ApplicationException
	{
		public ApplicationExceptionBase(string message)
			: base(message) { }
	}
}

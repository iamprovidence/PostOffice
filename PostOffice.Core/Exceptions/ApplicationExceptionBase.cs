using System;

namespace PostOffice.Core.Exceptions
{
	public abstract class ApplicationExceptionBase : Exception
	{
		public ApplicationExceptionBase(string message)
			: base(message) { }
	}
}

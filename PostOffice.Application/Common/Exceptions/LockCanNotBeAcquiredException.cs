using PostOffice.Core.Exceptions;

namespace PostOffice.Application.Common.Exceptions
{
	public class LockCanNotBeAcquiredException : ApplicationExceptionBase
	{
		public LockCanNotBeAcquiredException(string message)
			: base(message){ }
	}
}

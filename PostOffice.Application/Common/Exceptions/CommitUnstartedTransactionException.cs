using PostOffice.Core.Exceptions;

namespace PostOffice.Application.Common.Exceptions
{
	public class CommitUnstartedTransactionException : ApplicationExceptionBase
	{
		public CommitUnstartedTransactionException()
			: this("You can not commit transaction when transaction was not started") { }
		public CommitUnstartedTransactionException(string message)
			: base(message) { }
	}
}

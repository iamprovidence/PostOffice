using PostOffice.Core.Exceptions;

namespace PostOffice.Application.Common.Exceptions
{
	public class RollbackUnstartedTransactionException : ApplicationExceptionBase
	{
		public RollbackUnstartedTransactionException()
			: this("You can not rollback transaction when transaction was not started") { }
		public RollbackUnstartedTransactionException(string message)
			: base(message) { }
	}
}

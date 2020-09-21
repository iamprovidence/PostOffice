namespace PostOffice.Domain.Exceptions
{
	public class TtnShouldContainOnlyDigitsException : Core.Exceptions.DomainExceptionBase
	{
		public TtnShouldContainOnlyDigitsException(string message)
			: base(message) { }
		public TtnShouldContainOnlyDigitsException()
			: base("TTN contains unexpected chapters.") { }
	}
}

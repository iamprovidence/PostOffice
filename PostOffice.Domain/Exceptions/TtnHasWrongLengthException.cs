namespace PostOffice.Domain.Exceptions
{
	public class TtnHasWrongLengthException : Core.Exceptions.DomainExceptionBase
	{
		public TtnHasWrongLengthException(string message)
			: base(message) { }
		public TtnHasWrongLengthException(int expectedLength, int currentLength)
			: base($"TTN has wrong length. Expected length {expectedLength}, meanwhile current length is {currentLength}") { }
	}
}

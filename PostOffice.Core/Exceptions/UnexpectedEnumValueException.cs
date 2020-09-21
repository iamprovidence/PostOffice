using System;

namespace PostOffice.Core.Exceptions
{
	public class UnexpectedEnumValueException<TEnum> : Exception
	{
		public UnexpectedEnumValueException(TEnum enumValue)
			: base($"Value {enumValue} of {typeof(TEnum).Name} has not been expected") { }
	}
}

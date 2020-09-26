using PostOffice.Core.Entities;
using PostOffice.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostOffice.Domain.ValueObjects
{
	public class TTN : ValueObjectBase
	{
		private static readonly int TTN_LENGTH = 16;

		public string Value { get; }

		public TTN(string value)
		{
			ValidateTTN(value);

			Value = value;
		}

		public static void ValidateTTN(string value)
		{
			if (!value.All(char.IsDigit))
			{
				throw new TtnShouldContainOnlyDigitsException();
			}

			if (value.Length != TTN_LENGTH)
			{
				throw new TtnHasWrongLengthException(expectedLength: TTN_LENGTH, currentLength: value.Length);
			}
		}

		public static TTN Generate()
		{
			var random = new Random();
			var value = string.Concat(Enumerable.Range(0, TTN_LENGTH).Select(v => random.Next(9)));
			return new TTN(value);
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Value;
		}
	}
}

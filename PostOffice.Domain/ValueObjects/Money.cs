using PostOffice.Core.Entities;
using PostOffice.Domain.Enums;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class Money : ValueObjectBase
	{
		private decimal Amount { get; }
		private Currency Currency { get; }

		public Money(decimal amount, Currency currency)
		{
			Amount = amount;
			Currency = currency;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Amount;
			yield return Currency;
		}
	}
}

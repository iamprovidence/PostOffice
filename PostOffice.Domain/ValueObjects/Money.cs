using PostOffice.Core.Entities;
using PostOffice.Domain.Enums;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class Money : ValueObjectBase
	{
		private decimal Amount { get; set; }
		private Currency Currency { get; set; }

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

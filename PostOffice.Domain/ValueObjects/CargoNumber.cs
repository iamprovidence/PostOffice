using PostOffice.Core.Entities;
using System;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class CargoNumber : ValueObjectBase
	{
		public Guid Number { get; }

		public CargoNumber(Guid number)
		{
			Number = number;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Number;
		}
	}
}

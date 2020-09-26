using PostOffice.Core.Entities;
using System;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class CargoNumber : ValueObjectBase
	{
		public Guid Number { get; }

		private CargoNumber(Guid number)
		{
			Number = number;
		}

		public static CargoNumber GenerateNew()
		{
			return new CargoNumber(Guid.NewGuid());
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Number;
		}
	}
}

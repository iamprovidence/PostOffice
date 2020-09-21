using PostOffice.Core.Entities;
using System;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class Location : ValueObjectBase
	{
		public string City { get; }
		public string Street { get; }

		public Location(string city, string street)
		{
			City = city;
			Street = street;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return City;
			yield return Street;
		}
	}
}

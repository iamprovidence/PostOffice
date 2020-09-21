using PostOffice.Core.Entities;
using System.Collections.Generic;

namespace PostOffice.Domain.ValueObjects
{
	public class PackageSize : ValueObjectBase
	{
		public double Width { get; }
		public double Height { get; }
		public double Length { get; }

		public PackageSize(double width, double height, double length)
		{
			Width = width;
			Height = height;
			Length = length;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Width;
			yield return Height;
			yield return Length;
		}
	}
}

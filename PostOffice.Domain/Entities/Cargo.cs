using PostOffice.Core.Entities;
using PostOffice.Domain.ValueObjects;

namespace PostOffice.Domain.Entities
{
	public class Cargo : IEntity<CargoNumber>
	{
		public CargoNumber Identifier { get; private set; }
		public PackageSize PackageSize { get; private set; }

		public static Cargo CreateNew(double width, double height, double length)
		{
			return new Cargo
			{
				Identifier = CargoNumber.GenerateNew(),
				PackageSize = new PackageSize(width, height, length),
			};
		}
	}
}

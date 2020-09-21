using PostOffice.Core.Entities;
using PostOffice.Domain.ValueObjects;

namespace PostOffice.Domain.Entities
{
	public class Cargo : IEntity<CargoNumber>
	{
		public CargoNumber Identifier { get; }
		public PackageSize PackageSize { get; }
	}
}

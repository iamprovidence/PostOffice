using MongoDB.Bson.Serialization;
using PostOffice.Domain.ValueObjects;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;

namespace PostOffice.Infrastructure.Persistence.Configurations
{
	internal class CargoNumberConfiguration : EntityConfigurationBase<CargoNumber>
	{
		protected override void Configure(BsonClassMap<CargoNumber> builder)
		{
			builder.AutoMap();

			builder.MapProperty(x => x.Number);
		}
	}
}

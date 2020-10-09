using MongoDB.Bson.Serialization;
using PostOffice.Domain.ValueObjects;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;

namespace PostOffice.Infrastructure.Persistence.Configurations
{
	internal class TtnConfiguration : EntityConfigurationBase<TTN>
	{
		protected override void Configure(BsonClassMap<TTN> builder)
		{
			builder.AutoMap();

			builder.MapCreator(x => new TTN(x.Value));

			builder.MapProperty(x => x.Value);
		}
	}
}

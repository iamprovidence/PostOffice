using MongoDB.Bson.Serialization;
using PostOffice.Domain.ValueObjects;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;

namespace PostOffice.Infrastructure.Persistence.Configurations
{
	internal class MoneyConfiguration : EntityConfigurationBase<Money>
	{
		protected override void Configure(BsonClassMap<Money> builder)
		{
			builder.AutoMap();

			builder.MapProperty("Amount");
			builder.MapProperty("Currency");
		}
	}
}

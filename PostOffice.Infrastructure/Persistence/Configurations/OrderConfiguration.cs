using MongoDB.Bson.Serialization;
using PostOffice.Domain.Entities;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;

namespace PostOffice.Infrastructure.Persistence.Configurations
{
	internal class OrderConfiguration : EntityConfigurationBase<Order>
	{
		protected override void Configure(BsonClassMap<Order> builder)
		{
			builder.MapProperty(x => x.Identifier);
			builder.MapProperty(x => x.Price);
			builder.MapProperty(x => x.Description);
			builder.MapProperty(x => x.Status);

			builder.MapProperty(x => x.SenderLocation);
			builder.MapProperty(x => x.RecipientLocation);
			builder.MapProperty(x => x.CurrentLocation);

			builder.MapField("_cargos").SetElementName(nameof(Order.Cargos));
		}
	}
}

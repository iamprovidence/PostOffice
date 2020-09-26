using MongoDB.Bson.Serialization;

namespace PostOffice.Infrastructure.Persistence.Configurations.Abstract
{
	internal abstract class EntityConfigurationBase<TEntity>
	{
		protected EntityConfigurationBase()
		{
			BsonClassMap.RegisterClassMap<TEntity>(Configure);
		}

		protected abstract void Configure(BsonClassMap<TEntity> builder);
	}
}

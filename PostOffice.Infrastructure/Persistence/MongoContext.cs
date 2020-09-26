using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostOffice.Core.Entities;
using PostOffice.Infrastructure.Configuration;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace PostOffice.Infrastructure.Persistence
{
	public class MongoContext
	{
		private readonly IMongoDatabase _database;

		public MongoContext(IOptions<MongoDbContextConfiguration> options)
		{
			var configuration = options.Value;
			var client = new MongoClient(configuration.ConnectionString);
			_database = client.GetDatabase(configuration.DatabaseName);
		}

		static MongoContext()
		{
			// TODO: write all configuration classes explicitly
			ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public IMongoCollection<TEntity> Collection<TEntity>()
			where TEntity : class, IAggregateRoot
		{
			var collectionName = typeof(TEntity).GetCustomAttribute<TableAttribute>().Name;

			return _database.GetCollection<TEntity>(collectionName);
		}

		public static void ApplyConfigurationsFromAssembly(Assembly assembly)
		{
			foreach (var configuration in LoadConfigurationsFromAssembly(assembly))
			{
				Activator.CreateInstance(configuration);
			}
		}

		private static IEnumerable<Type> LoadConfigurationsFromAssembly(Assembly assembly)
		{
			return assembly
				.GetTypes()
				.Where(t => t.IsClass)
				.Where(t => !t.IsAbstract)
				.Where(t => t.BaseType != null)
				.Where(t => t.BaseType.IsGenericType)
				.Where(t => t.BaseType.GetGenericTypeDefinition() == typeof(EntityConfigurationBase<>));
		}
	}
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostOffice.Application.Common.Exceptions;
using PostOffice.Application.Common.Persistence;
using PostOffice.Core.Entities;
using PostOffice.Infrastructure.Persistence.Configurations.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Persistence
{
	public class MongoContext : IUnitOfWork
	{
		private readonly MongoClient _mongoClient;
		private readonly IMongoDatabase _database;

		public MongoContext(IOptions<MongoDbContextConfiguration> options)
		{
			var configuration = options.Value;
			_mongoClient = new MongoClient(configuration.ConnectionString);
			_database = _mongoClient.GetDatabase(configuration.DatabaseName);
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

		#region UnitOfWork

		public IClientSessionHandle Session { get; private set; }
		public async Task BeginTransactionAsync(CancellationToken cancellationToken)
		{
			Session = await _mongoClient.StartSessionAsync(cancellationToken: cancellationToken);

			// TODO: implement transaction
			/*Session.StartTransaction(new TransactionOptions(
				readPreference: ReadPreference.Primary,
				readConcern: ReadConcern.Local,
				writeConcern: WriteConcern.WMajority)
			);*/
		}

		public async Task CommitTransactionAsync(CancellationToken cancellationToken)
		{
			if (Session == null) throw new CommitUnstartedTransactionException();

			// await Session.CommitTransactionAsync(cancellationToken);
			Session.Dispose();

			Session = null;
		}

		public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
		{
			if (Session == null) throw new RollbackUnstartedTransactionException();

			// await Session.AbortTransactionAsync(cancellationToken);
			Session.Dispose();

			Session = null;
		}
		#endregion
	}
}

using MongoDB.Driver;
using PostOffice.Application.Common.Persistence;
using PostOffice.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice.Infrastructure.Persistence
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IMongoCollection<Order> _orderCollection;

		public OrderRepository(MongoContext mongoContext)
		{
			_orderCollection = mongoContext.Collection<Order>();
		}

		public Task AddAsync(Order order, CancellationToken cancellationToken)
		{
			var options = new InsertOneOptions
			{
				BypassDocumentValidation = false,
			};

			return _orderCollection.InsertOneAsync(order, options, cancellationToken);
		}
	}
}

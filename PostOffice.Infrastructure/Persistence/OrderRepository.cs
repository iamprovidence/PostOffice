using MongoDB.Driver;
using PostOffice.Application.Common.Persistence;
using PostOffice.Application.Common.ViewModels;
using PostOffice.Application.Orders.ViewModels;
using PostOffice.Domain.Entities;
using System.Collections.Generic;
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

		public async Task<bool> DeleteAsync(string ttn, CancellationToken cancellationToken)
		{
			var deleteResult = await _orderCollection.DeleteOneAsync(x => x.Identifier.Value == ttn, cancellationToken);

			return deleteResult.DeletedCount > 0;
		}

		public async Task<IReadOnlyCollection<OrderListItemViewModel>> FindOrdersAsync(CancellationToken cancellationToken)
		{
			var filter = Builders<Order>.Filter.Empty;

			var projection = Builders<Order>.Projection.Expression(p => new OrderListItemViewModel
			{
				Ttn = p.Identifier.Value,
				Status = p.Status,
				Description = p.Description,
				CurrentLocation = new LocationViewModel
				{
					Street = p.CurrentLocation.Street,
					City = p.CurrentLocation.City,
				},
			});

			return await _orderCollection
				.Find(filter)
				.Project(projection)
				.ToListAsync(cancellationToken);
		}
	}
}

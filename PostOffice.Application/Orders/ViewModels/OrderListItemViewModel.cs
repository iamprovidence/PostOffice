using PostOffice.Application.Common.ViewModels;
using PostOffice.Domain.Enums;

namespace PostOffice.Application.Orders.ViewModels
{
	public class OrderListItemViewModel
	{
		public string Ttn { get; set; }
		public string Description { get; set; }
		public OrderStatus Status { get; set; }
		public LocationViewModel CurrentLocation { get; set; }
	}
}

using PostOffice.Application.Common.ViewModels;

namespace PostOffice.Application.Orders.ViewModels
{
	public class OrderLocationChangedViewModel
	{
		public string Ttn { get; set; }
		public LocationViewModel Location { get; set; }
	}
}

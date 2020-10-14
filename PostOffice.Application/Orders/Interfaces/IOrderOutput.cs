using PostOffice.Application.Common.OutputPort;
using PostOffice.Application.Orders.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.Application.Orders.Interfaces
{
	public interface IOrderOutput : IOutputPort
	{
		Task OrdersLoaded(IEnumerable<OrderListItemViewModel> orderList);
		Task OrderDeleted(string ttn);
		Task OrderLocationChanged(OrderLocationChangedViewModel viewModel);
	}
}

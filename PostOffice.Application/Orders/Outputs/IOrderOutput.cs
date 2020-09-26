using System.Threading.Tasks;

namespace PostOffice.Application.Orders.Output
{
	public interface IOrderOutput
	{
		Task OrderCreated(int v);
	}
}

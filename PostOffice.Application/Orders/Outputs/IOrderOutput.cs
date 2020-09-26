using System.Threading.Tasks;

namespace PostOffice.Application.Orders.Outputs
{
	public interface IOrderOutput
	{
		Task OrderCreated(int v);
	}
}

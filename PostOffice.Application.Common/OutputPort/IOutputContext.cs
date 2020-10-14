namespace PostOffice.Application.Common.OutputPort
{
	public interface IOutputContext<out TOutputPort>
		where TOutputPort : class, IOutputPort
	{
		public TOutputPort ResponseWith();
		public TOutputPort NotifyAll();
	}
}

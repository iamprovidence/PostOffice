namespace PostOffice.Core.Entities
{
	public interface IEntity<out T>
	{
		public T Identifier { get; }
	}
}

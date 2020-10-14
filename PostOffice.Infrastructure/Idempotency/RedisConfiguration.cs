namespace PostOffice.Infrastructure.Idempotency
{
	public class RedisConfiguration
	{
		public string InstanceName { get; set; }
		public string ConnectionString { get; set; }
	}
}

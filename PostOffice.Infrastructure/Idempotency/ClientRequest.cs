using System;

namespace PostOffice.Infrastructure.Idempotency
{
	public class ClientRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime Time { get; set; }
	}
}

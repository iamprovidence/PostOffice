using System;

namespace PostOffice.Infrastructure.Idempotency
{
	internal class ClientRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime Time { get; set; }
	}
}

using MediatR;

namespace PostOffice.Application.Common.Idempotency
{
	public static class LockKeyHelper
	{
		public static string GetKey(IRequest request)
		{
			return request.GetType().Name;
		}
	}
}

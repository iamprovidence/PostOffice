using Microsoft.AspNetCore.Http;
using PostOffice.Application.Common.Identity;
using System.Threading.Tasks;

namespace PostOffice.API.Configurations
{
	public class UserContextMiddleware : IMiddleware
	{
		private readonly IUserContextSettable _userContextSettable;

		public UserContextMiddleware(IUserContextSettable userContextSettable)
		{
			_userContextSettable = userContextSettable;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			// TODO: get value from headers
			_userContextSettable.SetContext("random");
			await next.Invoke(context);
		}
	}
}

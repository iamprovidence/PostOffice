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

		public Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			_userContextSettable.SetContext("random");
			return Task.CompletedTask;
		}
	}
}

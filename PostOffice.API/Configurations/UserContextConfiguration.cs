using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace PostOffice.API.Configurations
{
	internal static class UserContextConfiguration
	{
		public static void UseUserContext(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseMiddleware<UserContextMiddleware>();
		}
	}
}

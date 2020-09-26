using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Identity;
using PostOffice.Infrastructure.Identity;

namespace PostOffice.API.Configurations
{
	// TODO: move to infrastructure to make implementations private
	internal static class UserContextConfiguration
	{
		public static void AddUserContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<UserContext>();
			services.AddScoped<IReadOnlyUserContext>(sp => sp.GetRequiredService<UserContext>());
			services.AddScoped<IUserContext>(sp => sp.GetRequiredService<UserContext>());
			services.AddScoped<IUserContextSettable, UserContextSettable>();

			services.AddTransient<UserContextMiddleware>();
		}

		public static void UseUserContext(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseMiddleware<UserContextMiddleware>();
		}
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Common.Identity;

namespace PostOffice.Infrastructure.Identity
{
	public static class IdentityConfiguration
	{
		public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<UserContext>();
			services.AddScoped<IReadOnlyUserContext>(sp => sp.GetRequiredService<UserContext>());
			services.AddScoped<IUserContext>(sp => sp.GetRequiredService<UserContext>());
			services.AddScoped<IUserContextSettable, UserContextSettable>();

			services.AddTransient<UserContextMiddleware>();
		}

		public static void UseIdentity(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseMiddleware<UserContextMiddleware>();
		}
	}
}

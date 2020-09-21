using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostOffice.Angular.Configurations
{
	internal static class MvcConfiguration
	{
		public static void AddMvc(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllersWithViews();
		}

		public static void UseMvc(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});
		}
	}
}

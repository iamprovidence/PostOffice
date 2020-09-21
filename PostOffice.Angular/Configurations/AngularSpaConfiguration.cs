using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PostOffice.Angular.Configurations
{
	internal static class AngularSpaConfiguration
	{
		public static void AddAngularSpa(this IServiceCollection services, IConfiguration configuration)
		{
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		public static void UseAngularSpa(this IApplicationBuilder app, IWebHostEnvironment environment)
		{
			if (!environment.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (environment.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}

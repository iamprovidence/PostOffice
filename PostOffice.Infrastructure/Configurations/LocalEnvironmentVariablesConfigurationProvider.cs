using DotNetEnv;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostOffice.Infrastructure.Configurations
{
	internal class LocalEnvironmentVariablesConfigurationProvider : ConfigurationProvider
	{

		public override void Load()
		{
			Data = LoadSettings();
		}
		public IDictionary<string, string> LoadSettings()
		{
			if (!IsDevelopmentEnvironment()) return Enumerable.Empty<string>().ToDictionary(k => k, v => v);

			var filePath = GetEnvFilePath();
			if (string.IsNullOrWhiteSpace(filePath)) return Enumerable.Empty<string>().ToDictionary(k => k, v => v);

			return File.ReadLines(filePath)
				.Where(line => !string.IsNullOrWhiteSpace(line))
				.Select(line =>
				{
					var keyValue = line.Split("=");

					return new
					{
						key = keyValue[0],
						value = keyValue[1],
					};
				})
				.ToDictionary(k => k.key.Replace("__", ":"), v => v.value);
		}

		private bool IsDevelopmentEnvironment()
		{
			var environment = Env.GetString("ASPNETCORE_ENVIRONMENT");

			return (environment ?? string.Empty).Equals("DEVELOPMENT", StringComparison.OrdinalIgnoreCase);
		}

		private string GetEnvFilePath()
		{
			var filePath = Path.Combine(GetSolutionFolder(), ".env");

			return File.Exists(filePath) ? filePath : string.Empty;
		}

		private string GetSolutionFolder()
		{
			var folderPath = ".";

			while (!IsSolutionRoot(folderPath))
			{
				folderPath = Directory.GetParent(folderPath).FullName;
			}

			return folderPath;
		}

		private bool IsSolutionRoot(string path)
		{
			return Directory.GetFiles(path, "*.sln").Length > 0;
		}
	}
}

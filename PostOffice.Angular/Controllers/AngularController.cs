using Microsoft.AspNetCore.Mvc;
using PostOffice.Angular.Application.Services;

namespace PostOffice.Angular.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AngularController : Controller
	{
		private readonly SettingsAppService _settingsAppService;

		public AngularController(SettingsAppService settingsAppService)
		{
			_settingsAppService = settingsAppService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var environmentData = _settingsAppService.GetEnvironmentData();

			return View(environmentData);
		}
	}
}

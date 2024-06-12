using Microsoft.AspNetCore.Mvc;

namespace LocalChat.UI.Controllers
{
	public class ReactionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

	}
}

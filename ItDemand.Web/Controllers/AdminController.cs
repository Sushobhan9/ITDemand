using Microsoft.AspNetCore.Mvc;

namespace ItDemand.Web.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

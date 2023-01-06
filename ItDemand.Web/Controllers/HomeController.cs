using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ItDemand.Web.Controllers
{
	public class HomeController : Controller
	{
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;
        private readonly IMapper _mapper;

		public HomeController(ApplicationLog log, ItDemandContext dbContext, IMapper mapper)
		{
            _db = dbContext;
            _log = log;
            _mapper = mapper;           
        }

		[HttpGet]
        public JsonResult Demands(int businessUnitFilter, WorkflowType projectType, DemandState demandState)
        {
            var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
            var demands = demandService.GetDemandList(businessUnitFilter, projectType, demandState);
            return Json(new { data = demands });
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
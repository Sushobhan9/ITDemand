using ItDemand.Domain.DataContext;
using ItDemand.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItDemand.Web.Controllers
{
    public class TelemetryController : Controller
	{
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;

        public TelemetryController(ApplicationLog log, ItDemandContext dbContext)
        {
            _db = dbContext;
            _log = log;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
		public async Task<JsonResult> LogEntries()
		{
            // https://codewithmukesh.com/blog/jquery-datatable-in-aspnet-core/
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault() ?? "entryDate";
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault() ?? "desc";
                var searchValue = Request.Form["search[value]"].FirstOrDefault() ?? string.Empty;
               
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var logService = new TelemetryLogService(_db);
                var records = await logService.GetLogEntries(skip, pageSize, sortColumn, sortDirection, searchValue);
                int recordsTotal = logService.GetTotalCount();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = records };

                return Json(jsonData);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false });
            }          
        }
	}
}

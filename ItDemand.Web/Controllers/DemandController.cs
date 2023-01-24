using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Utils;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;

namespace ItDemand.Web.Controllers
{
    public class DemandController : Controller
	{
        private readonly ApplicationLog _log;
		private readonly IMapper _mapper;
        private readonly ItDemandContext _db;

        public DemandController(ApplicationLog log, ItDemandContext dbContext, IMapper mapper)
		{
			_db = dbContext;
			_mapper = mapper;
            _log = log;
        }

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult DemandRequestForm(int? id)
		{
			var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
			var vm = demandService.GetDemandRequest(id);
            if (vm == null) return View();
            return vm.ExecutionType == null ? View("DemandPage", vm) : PartialView("DemandRequestForm", vm);
		}

        [HttpPost]
		public JsonResult Save([FromForm] DemandRequestViewModel request)
		{
			try
			{
				var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
				var demandId = demandService.SaveRequest(request);
				return Json(new { success = true, redirectUrl = "/Home", demandId });
			}
			catch (Exception ex)
			{
				_log.Error(ex);
				return Json(new { success = false, message = ex.Message });
			}
			
		}

        [HttpGet]
		public JsonResult BusinessUnits()
		{
			try
			{
				var selectOptionsService = new SelectOptionsService(_db, _mapper);
				var businessUnitOptions = selectOptionsService.GetBusinessUnitOptions().ToArray();
				return Json(new { success = true, options = businessUnitOptions });
			}
			catch (Exception ex)
			{
				_log.Error(ex);
				return Json(new { success = false, message = ex.Message });
			}
		}

        [HttpGet]
        public JsonResult DemandStates()
		{
			try
			{
				var items = new List<SelectListItem>();
                foreach (DemandState item in Enum.GetValues(typeof(DemandState)))
                {
					items.Add(new SelectListItem
                    {
                        Text = item.GetDescription<DemandState>(),
                        Value = Convert.ToInt32(item).ToString()
                    });
                }
                return Json(new { success = true, options = items });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetItHead(int businessUnitId)
        {
            try
            {
                var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
                var itHead = demandService.GetItHeadForBusinessUnit(businessUnitId);
                return Json(new { success = true, itHead });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult BusinessProcessL2Options(int parentId)
        {
            try
            {
                var selectOptionsService = new SelectOptionsService(_db, _mapper);
                var options = selectOptionsService.GetBusinessProcessL2Options(parentId).OrderBy(x => x.Name).ToArray();
                return Json(new { success = true, options });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult BusinessProcessL3Options(int parentId)
        {
            try
            {
                var selectOptionsService = new SelectOptionsService(_db, _mapper);
                var options = selectOptionsService.GetBusinessProcessL3Options(parentId).OrderBy(x => x.Name).ToArray();
                return Json(new { success = true, options });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Cancel(int id)
        {
            try
            {
                var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
                demandService.CancelRequest(id);
                return Json(new { success = true, redirectUrl = $"/Demand/DemandRequestForm/{id}" });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Reinstate(int id)
        {
            try
            {
                var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
                demandService.ReinstateRequest(id);
                return Json(new { success = true, redirectUrl = $"/Demand/DemandRequestForm/{id}" });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
		public JsonResult Delete(int id)
		{
            try
            {
                _db.DeleteDemand(id, (model) => _log.Info($"Demand Request #{id} - \"{model.Name}\" has been deleted."));
                return Json(new { success = true, redirectUrl = "/Home" });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _db.Attachments.FindAsync(id);
            if (file == null)
            {
                _log.Warn($"File Download Error: Attachment with Id={id} not found.");
                return NotFound();
            }

            return File(file.Contents, GetContentType(file.FileName ?? ""), file.FileName);
        }

        private static string GetContentType(string filename)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filename, out var contentType))
                contentType = "application/octet-stream";
            return contentType;
        }
    }
}

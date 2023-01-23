using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItDemand.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationLog _log;
        private readonly IMapper _mapper;
        private readonly ItDemandContext _db;

        public ProjectController(ApplicationLog log, ItDemandContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
            _log = log;
        }

        public IActionResult Index(int id)
        {
            try
            {
                var project =
                    _db.DemandRequests
                        .Include(x => x.Checklists)
                            .ThenInclude(x => x.WorkflowItem)
                        .SingleOrDefault(x => x.Id == id);

                if (project == null) return View(ProjectViewModel.Empty);

                var vm = new ProjectViewModel
                {
                    Id = project.Id,
                    Name = project.Name,
                    Checklists = _mapper.Map<ICollection<Checklist>, ChecklistViewModel[]>(project.Checklists)
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public IActionResult Status(int id)
        {
            try
            {
                var project = 
                    _db.DemandRequests
                        .Include(x => x.Checklists)
                            .ThenInclude(x => x.WorkflowItem)
                        .SingleOrDefault(x => x.Id == id);

                if (project == null) return PartialView(ProjectViewModel.Empty);

                var vm = new ProjectViewModel 
                { 
                    Id = project.Id, 
                    Name = project.Name, 
                    Checklists = _mapper.Map<ICollection<Checklist>, ChecklistViewModel[]>(project.Checklists) };

                return PartialView(vm);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public IActionResult Checklist(int id)
        {
            var checklistService = new ChecklistService(_log, _db, _mapper, this.GetUser());
            var vm = checklistService.GetChecklist(id);
            return PartialView(vm);
        }

        [HttpPost]
        public JsonResult ChecklistSave([FromForm] ChecklistFormViewModel vm)
        {
			// https://www.learnrazorpages.com/razor-pages/forms/checkboxes
			try
			{
                var checklistService = new ChecklistService(_log, _db, _mapper, this.GetUser());
                checklistService.SaveChecklist(vm);
                return Json(new { success = true, demandId = vm.DemandRequestId });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult WorkflowData(int id)
        {
            var vm = new ProjectWorkflowViewModel(_db, id);
            return Json(vm.FindWorkflowData());
        }
    }
}

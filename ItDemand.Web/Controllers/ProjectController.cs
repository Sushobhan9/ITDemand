using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var project = _db.DemandRequests.Include(x => x.Checklists).SingleOrDefault(x => x.Id == id);
                if (project == null) return View(ProjectViewModel.Empty);

                var vm = new ProjectViewModel { Id = project.Id, Name = project.Name, Checklists = _mapper.Map<ICollection<Checklist>, ChecklistViewModel[]>(project.Checklists) };

                return View(vm);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }
    }
}

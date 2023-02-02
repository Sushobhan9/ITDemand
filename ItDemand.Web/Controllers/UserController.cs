using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ItDemand.Web.Controllers
{
	public class UserController : Controller
	{
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;
		private readonly IMapper _mapper;

		public UserController(ApplicationLog log, ItDemandContext dbContext, IMapper mapper)
		{
			_db = dbContext;
			_mapper = mapper;
            _log = log;
        }

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Current()
		{
			UserViewModel userViewModel;
			var userIdentity = HttpContext.User.Identity;

			if (userIdentity == null)
				throw new InvalidOperationException("Unable to determine current user identity. User Principle Identity is null.");

			if (string.IsNullOrEmpty(userIdentity.Name))
				throw new InvalidOperationException("Unable to determine current user identity. User Principle.Name is empty.");

            var userService = new UserService(_db, _mapper);
			var userModel = userService.GetUserByUserName(userIdentity.Name);

			if (userModel != null)
			{
                userViewModel = _mapper.Map<UserViewModel>(userModel);
                return Ok(userViewModel);
            }

			// User not found in Users table, query AD and use the returned info to create a User table entry.
            var adService = new ActiveDirectoryService();
			var account = adService.FindUser(samaccountname: userIdentity.Name);
            userModel = userService.VerifyOrCreateUser(account);
			_db.SaveChanges();

			// ToDo: if the user is unable to be located in AD, return a Not Found error to display to the current user
            userViewModel = _mapper.Map<UserViewModel>(userModel);
            return Ok(userViewModel);
        }

		public IActionResult Search(string query, string attribute)
		{
			var results = ActiveDirectoryService.FindUsers(query, attribute);
			return Ok(results);
		}

        [HttpPost]
        public async Task<JsonResult> Users()
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

                var userService = new UserService(_db, _mapper);
                var records = await userService.GetPagedUsers(skip, pageSize, sortColumn, sortDirection, searchValue);
                int recordsTotal = userService.GetUserCount();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = records };

                return Json(jsonData);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false });
            }
        }

        public IActionResult EditUser(int id)
        {
            var userService = new UserService(_db, _mapper);
            var user = userService.GetUserById(id);
            var vm = _mapper.Map<UserViewModel>(user);
            return View(vm);
        }

        [HttpPost]
        public JsonResult SaveUser(UserViewModel vm)
        {
            //Change the submit to fetch api and return Json result
            
            try
            {
                // Preprocess the checkboxes for Security Role Type.
                // Need to convert from the array sent to a single value enum flag.
                Request.Form.TryGetValue("SecurityRole", out StringValues securityRoles);
                vm.SecurityRole = (SecurityRole)Enum.Parse(typeof(SecurityRole), securityRoles);

                var userService = new UserService(_db, _mapper);

                User? user;
                if (vm.Id > 0)
                {
                    user = userService.Update(vm);
                }
                else
                {
                    user = userService.Add(vm);
                }

                _db.SaveChanges();
                vm = _mapper.Map<UserViewModel>(user);

                return Json(new { success = true, vm.Id });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false });
            }            
        }

        [HttpGet]
        public IActionResult MyPortfolio()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MyApprovals()
        {
            try
            {
                var statusFilter = Request.Form["statusFilter"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault() ?? "entryDate";
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault() ?? "desc";
                var searchValue = Request.Form["search[value]"].FirstOrDefault() ?? string.Empty;

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int checklistStatus = statusFilter != null ? Convert.ToInt32(statusFilter) : 99;

                var checklistService = new ChecklistService(_log, _db, _mapper, this.GetUser());
                var pagedRows = checklistService.GetApprovalsForUser(skip, pageSize, sortColumn, sortDirection, searchValue, (StatusType)checklistStatus);
                var jsonData = new { draw, recordsFiltered = pagedRows.TotalCount, recordsTotal = pagedRows.TotalCount, data = pagedRows.Rows };

                return Json(jsonData);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult MyDemandRequests()
        {
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

                var demandService = new DemandService(_log, _db, _mapper, this.GetUser());
                var pagedRows = demandService.GetDemandsForUser(skip, pageSize, sortColumn, sortDirection, searchValue);
                var jsonData = new { draw, recordsFiltered = pagedRows.TotalCount, recordsTotal = pagedRows.TotalCount, data = pagedRows.Rows };

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

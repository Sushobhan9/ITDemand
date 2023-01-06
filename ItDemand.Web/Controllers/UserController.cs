using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Web.Services;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItDemand.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly ItDemandContext _db;
		private readonly IMapper _mapper;

		public UserController(ItDemandContext dbContext, IMapper mapper)
		{
			_db = dbContext;
			_mapper = mapper;
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
			var userModel = userService.GetEmployeeByUserName(userIdentity.Name);

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

		//[HttpGet, Route("Search")]
		public IActionResult Search(string query, string attribute)
		{
			var results = ActiveDirectoryService.FindUsers(query, attribute);
			return Ok(results);
		}

        //public ActionResult UserNameDisplay(string badge)
        //{
        //    string userName = string.Empty;
        //    if (badge != null)
        //    {
        //        int oncid;
        //        if (Int32.TryParse(badge.Substring(2), out oncid))
        //        {
        //            Core.Employee employee = Data.Oracle.GetEmployee(oncid.ToString());
        //            if (employee != null)
        //            {
        //                userName = employee.FirstName + ' ' + employee.Name;
        //            }
        //        }
        //        else
        //            userName = "invalid badge";
        //    }
        //    ViewBag.UserName = userName;
        //    return PartialView("_Login");
        //}
    }
}

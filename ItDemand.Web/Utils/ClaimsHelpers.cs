using ItDemand.Web.Models;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ItDemand
{
	public static class ClaimsHelpers
	{
        public static IEnumerable<UserViewModel> ReadUserClaims(this HttpContext httpContext)
        {
            var claimsPrincipal = httpContext.User;
            var user = ReadClaim<UserViewModel>(claimsPrincipal, "UserData");
            yield return user ?? UserViewModel.Default;
        }

        public static T? ReadClaim<T>(this ClaimsPrincipal claimsPrincipal, string type)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == type);
            if (claim == null) return default;
            return JsonConvert.DeserializeObject<T>(claim.Value);
        }

        public static UserViewModel GetUser(this Controller controller) => ReadUserClaims(controller.HttpContext).LastOrDefault() ?? UserViewModel.Default;

        public static UserViewModel GetUser(this ViewComponent component) => ReadUserClaims(component.HttpContext).LastOrDefault() ?? UserViewModel.Default;
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ItDemand.Web.ViewComponents
{
    public class UserDisplayNameViewComponent : ViewComponent
    {
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var user = this.GetUser();

        //    return await Task.FromResult((IViewComponentResult)View("Default", user.DisplayName));
        //}

        public async Task<string> InvokeAsync()
        {
            var user = this.GetUser();
            return await Task.FromResult(user.DisplayName);
        }
    }
}

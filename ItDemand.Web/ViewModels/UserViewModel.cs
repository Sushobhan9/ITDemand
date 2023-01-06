

using ItDemand.Web.Models;

namespace ItDemand.Web.ViewModels
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string DisplayName { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public int SecurityRole { get; set; } = (int)Domain.Enums.SecurityRole.User;

        public static UserViewModel Default { get; } = new UserViewModel { DisplayName = "Unknown" };

    }
}

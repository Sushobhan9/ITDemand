using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string DisplayName { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public SecurityRole SecurityRole { get; set; } = SecurityRole.User;
        public bool IsActive { get; set; }

        public static UserViewModel Default { get; } = new UserViewModel { DisplayName = "Unknown" };

        public bool IsAdmin() => SecurityRole.HasFlag(SecurityRole.Admin);
        public bool IsPmo() => SecurityRole.HasFlag(SecurityRole.Pmo) || IsAdmin();
        public bool IsBusinessConsulting() => SecurityRole.HasFlag(SecurityRole.Consulting) || IsAdmin();

    }
}

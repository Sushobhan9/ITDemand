using ItDemand.Domain.Enums;

namespace ItDemand.Domain.Models
{
	public class User
	{
		public int Id { get; set; }
		public string DisplayName { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public SecurityRole SecurityRole { get; set; } = SecurityRole.User;

		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? LastModified { get; set; }

		public static User Default = new() { Id = 0, DisplayName = "Unknown User", UserName = "", Email = ""};

        public bool IsAdmin => SecurityRole == SecurityRole.Admin;
    }
}

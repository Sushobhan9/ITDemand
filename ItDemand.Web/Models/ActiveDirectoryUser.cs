using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;

namespace ItDemand.Web.Models
{
	public class ActiveDirectoryUser
	{
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string BusinessUnit { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public int UserAccountControl { get; set; } = 0;
        public bool IsDisabled { get; set; } = false;
        public string[] Group { get; set; } = Array.Empty<string>();
        public string DistinguishedName { get; set; } = string.Empty;
        public string CommonName { get; set; } = string.Empty;
        public string IdentityName { get; set; } = string.Empty;
        public string ObjectSid { get; set; } = string.Empty;
        public string UserPrincipalName { get; set; } = string.Empty;
        public string ObjectCategory { get; set; } = string.Empty;

        public static ActiveDirectoryUser Default { get; } = new ActiveDirectoryUser { DisplayName = "Unknown" };

        public User EnsureUserRecord(ItDemandContext db)
        {
            var record = db.Users.SingleOrDefault(x => x.UserName == UserName);

            if (record == null)
            {
                record = new User { UserName = UserName, SecurityRole = SecurityRole.User };
                db.Users.Add(record);
            }
            else
                record.LastModified = DateTimeOffset.Now;

            record.Email = Email;
            record.DisplayName = DisplayName;

			db.SaveChanges();
            return record;
        }
    }
}

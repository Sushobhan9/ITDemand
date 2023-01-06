namespace ItDemand.Web.Models
{
    public sealed class UserCache
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static UserCache() { }

        private UserCache() { }

        public static UserCache Instance { get; } = new UserCache();

        private readonly List<ActiveDirectoryUser> userAccounts = new List<ActiveDirectoryUser>();

        public void AddUserAccount(ActiveDirectoryUser userAccount)
        {
            var account = FindAccountByUserName(userAccount.UserName);

            if (account == null)
            {
                userAccounts.Add(userAccount);
            }
        }

        public bool RemoveUserAccount(ActiveDirectoryUser userAccount)
        {
            var account = FindAccountByUserName(userAccount.UserName);

            if (account == null) return false;
            userAccounts.Remove(userAccount);
            return true;
        }

        public ActiveDirectoryUser? FindAccountByUserName(string userName)
        {
            var account = userAccounts
                .SingleOrDefault(x => string.Equals(x.UserName, userName, StringComparison.CurrentCultureIgnoreCase));

            return account;
        }
    }
}

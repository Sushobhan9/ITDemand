using ItDemand.Web.Models;
using Linde.LoggingCore;
using Linde.LoggingCore.Providers.SqlServer;
using Microsoft.AspNetCore.Http.Features;
using System.Data.SqlClient;

namespace ItDemand.Web.Services
{
    public class ApplicationLog
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Log<ApplicationLogEntry> _logger;

        public ApplicationLog(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;

            _logger =
                LogFactory.GetSqlLogger<ApplicationLogEntry>
                    (new SqlOptions(configuration.GetConnectionString("ItDemand"), "ApplicationLog"));

            var httpContext = _contextAccessor.HttpContext;

            var principal = httpContext?.User;
            var userName = principal?.Identity.Name;

            ActiveDirectoryUser? currentUserAccount = null;

            if (!string.IsNullOrWhiteSpace(userName))
            {
                var userCache = UserCache.Instance;
                var userAccount = userCache.FindAccountByUserName(userName);

                if (userAccount != null)
                {
                    currentUserAccount = userAccount;
                }
                else
                {
                    // Use AD Lookup to get the current user's information
                    // and cache it.
                    var userAccounts = ActiveDirectoryService.FindUsers(userName);
                    currentUserAccount = userAccounts.SingleOrDefault(x => !x.IsDisabled);
                    if (currentUserAccount != null)
                    {
                        userCache.AddUserAccount(currentUserAccount);
                    }
                }
            }

            var httpRequestFeature = httpContext.Request.HttpContext.Features.Get<IHttpRequestFeature>();

            _logger.LogCreated += (sender, entry) =>
            {
                if (_contextAccessor == null) return;
                entry.Type = "Server";
                entry.HostAddress = httpContext?.Connection.RemoteIpAddress?.ToString() ?? "Not Available";
                //entry.Browser = httpContext.Request.Browser.Browser + "/" + httpContext.Request.Browser.Version;
                //entry.Browser = httpContext.Request.Headers["User-Agent"].ToString();
                entry.Url = httpRequestFeature?.RawTarget ?? "";

                if (currentUserAccount == null) return;
                entry.UserAccountName = currentUserAccount.UserName;
                entry.UserDisplayName = currentUserAccount.DisplayName;
                entry.UserBusinessUnit = currentUserAccount.BusinessUnit;
                entry.UserRegion = currentUserAccount.Country;
            };
        }
        public void Error(Exception ex) => _logger.Error(ex);
        public void Error(string formatString, params object[] args) => _logger.Error(formatString, args);
        public void Info(string formatString, params object[] args) => _logger.Info(formatString, args);
        public void Warn(string formatString, params object[] args) => _logger.Warn(formatString, args);
        public void Debug(string formatString, params object[] args) => _logger.Debug(formatString, args);
        public void Trace(string formatString, params object[] args) => _logger.Trace(formatString, args);
        public void Fatal(string formatString, params object[] args) => _logger.Fatal(formatString, args);

        public async Task<int> CreateLogEntry(ApplicationLogEntry logEntry)
        {
            var sql =
                @$"INSERT INTO ApplicationLog (
                    [Text]
                   ,[Category]
                   ,[EntryDate]
                   ,[Type]
                   ,[UserAccountName]
                   ,[UserDisplayName]
                   ,[UserRegion]
                   ,[UserBusinessUnit]
                   ,[Url]
                   ,[Browser]
                   ,[HostAddress]) 
                VALUES(
                    '{logEntry.Text}'
                   ,'{logEntry.Category}'
                   ,'{logEntry.EntryDate}'
                   ,'{logEntry.Type}'
                   ,'{logEntry.UserAccountName}'
                   ,'{logEntry.UserDisplayName}'
                   ,'{logEntry.UserRegion}'
                   ,'{logEntry.UserBusinessUnit}'
                   ,'{logEntry.Url}'
                   ,'{logEntry.Browser}'
                   ,'{logEntry.HostAddress}')";

            using var conn = new SqlConnection(_configuration.GetConnectionString("LeaAudit"));
            SqlCommand command = new(sql, conn);
            command.Connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            return result;
        }
    }
}

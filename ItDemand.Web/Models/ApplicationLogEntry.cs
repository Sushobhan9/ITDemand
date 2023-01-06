using Linde.LoggingCore.Models;

namespace ItDemand.Web.Models
{
    public class ApplicationLogEntry : LogEntry
    {
        public string Type { get; set; } = string.Empty;
        public string UserAccountName { get; set; } = string.Empty;
        public string UserDisplayName { get; set; } = string.Empty;
        public string UserRegion { get; set; } = string.Empty;
        public string UserBusinessUnit { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Browser { get; set; } = string.Empty;
        public string HostAddress { get; set; } = string.Empty;
    }
}

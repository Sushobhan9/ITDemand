using ItDemand.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ItDemand.Web.Services
{
	public class ActiveDirectoryService
	{
        public static ActiveDirectoryUser[] FindUsers(string query, string attribute = "samaccountname")
        {
            var proxyHttpClientHandler = new HttpClientHandler
            {
                UseProxy = false
            };

			using var client = new HttpClient(proxyHttpClientHandler);
			client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = client.GetAsync($"http://adlookup.le.grp/api/account/v3/?q={query}&attrs={attribute}").Result;
			if (!response.IsSuccessStatusCode) return Array.Empty<ActiveDirectoryUser>();

			var jsonResponse = response.Content.ReadAsStringAsync().Result;
			var accounts = JsonConvert.DeserializeObject<ActiveDirectoryUser[]>(jsonResponse);
			// exclude Disabled users from results
			accounts = accounts?.Where(x => !x.IsDisabled).ToArray() ?? Array.Empty<ActiveDirectoryUser>();
			return accounts;
		}

        public ActiveDirectoryUser? FindUser(string samaccountname)
        {
            try
            {
                return FindUsers(samaccountname).FirstOrDefault();
            }
            catch (Exception)
            {
                return new ActiveDirectoryUser { UserName = samaccountname };
            }
        }
    }
}

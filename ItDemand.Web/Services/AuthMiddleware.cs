using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Security.Principal;

namespace ItDemand.Web.Services
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;

		public AuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		[SupportedOSPlatform("windows")]
		public async Task InvokeAsync(HttpContext context, IMemoryCache cache, ItDemandContext db, ApplicationLog log, IMapper mapper)
		{
            UserViewModel userViewModel;

            if (context.User.Identity is not WindowsIdentity identity)
				throw new InvalidOperationException("User not a Windows identity. Is Windows authentication enabled?");

			// Use the cached value if its already there, this will avoid extra round trips to database to query user.
			if (cache.TryGetValue(identity.Name, out List<Claim> cachedClaims))
			{
				identity.AddClaims(cachedClaims);
			}
			else
			{
                var claims = new List<Claim>();
                var userService = new UserService(db, mapper);
                var user = userService.GetUserByUserName(identity.Name);

                if (user == null)
				{
                    // User not found in Users table, query AD and use the returned info to create a User table entry.
                    var adService = new ActiveDirectoryService();
                    var account = adService.FindUser(samaccountname: identity.Name);
                    user = userService.VerifyOrCreateUser(account);
                    db.SaveChanges();
                }

                userViewModel = mapper.Map<UserViewModel>(user);

                if (user.IsAdmin)
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                claims.Add(new Claim("UserData", JsonConvert.SerializeObject(userViewModel)));

                // Update the cache so they are available the next time through since this 
                // often runs multiple times per request.
                cache.Set(identity.Name, claims);
                identity.AddClaims(claims);
                log.Trace($"{user.DisplayName} has accessed the site.");
            }

			await _next(context); // Pass the request to the next middleware
		}
	}
}

using brevo_csharp.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DotNetGuide.Domain.Entities;

namespace DotNetGuide.Infrastructure.Middleware
{
    public class TenantIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public TenantIdMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.User.Identity.IsAuthenticated)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var userId = userManager.GetUserId(context.User);

                    if (userId != null)
                    {
                        var user = await userManager.FindByIdAsync(userId);

                        if (user != null)
                        {
                            var claimsIdentity = (ClaimsIdentity)context.User.Identity;

                            if (!context.User.HasClaim(c => c.Type == ClaimTypes.GivenName))
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName ?? "Unknown"));
                                
                                //Uncomment below link to see if its returning value in console.
                                //Console.WriteLine($"Added GivenName claim: {user.Name}");
                            }

                            // If you want to add more claims you can add as below. 

                            //if (!context.User.HasClaim(c => c.Type == "TenantId") && user.TenantId != null)
                            //{
                            //    claimsIdentity.AddClaim(new Claim("TenantId", user.TenantId.ToString()));
                            //}
                        }
                    }
                }
            }

            await _next(context);
        }
    }


}

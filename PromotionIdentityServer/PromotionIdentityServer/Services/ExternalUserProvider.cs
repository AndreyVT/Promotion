using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PromotionIdentityServer.Data;
using PromotionIdentityServer.Model;

namespace PromotionIdentityServer.Services
{
    public class ExternalUserProvider
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExternalUserProvider(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        internal async System.Threading.Tasks.Task<ExternalUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            var user = _applicationDbContext.ExternalUsers.Where(c => c.ExternalProvider == provider && c.ProviderUserId == providerUserId).FirstOrDefault();
            if (user == null)
            {
                string email = claims.ToList()[4].Value;

                ApplicationUser appUser;
                // first user, will be admin
                if (_applicationDbContext.ExternalUsers.Count() == 0)
                {
                    if ((appUser = _applicationDbContext.Users.Where(c => c.UserName == "admin").FirstOrDefault()) == null)
                    {
                        //admin
                        var adminUserApp = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = email
                        };
                        adminUserApp.Id = "admin";
                        var result3 = await _userManager.CreateAsync(adminUserApp, "111111");
                    }

                    //appUser = _applicationDbContext.Users.Where(c => c.UserName == "admin").FirstOrDefault();
                }
                else
                {
                    appUser = new ApplicationUser
                    {
                        UserName = claims.ToList()[4].Value.Substring(0, email.IndexOf('@')),
                        Email = email
                    };
                    appUser = _applicationDbContext.Users.Add(appUser).Entity;
                }

                ExternalUser newUser = new ExternalUser
                {
                    ExternalProvider = provider,
                    ProviderUserId = providerUserId,
                    Provider = provider,
                    Email = email,
                    User = appUser
                };
                user = _applicationDbContext.ExternalUsers.Add(newUser).Entity;
                _applicationDbContext.SaveChanges();
            }

            return user;
        }

        internal ExternalUser FindByExternalProvider(string provider, string providerUserId)
        {
            return _applicationDbContext.ExternalUsers.Where(c => c.ExternalProvider == provider && c.ProviderUserId == providerUserId).FirstOrDefault();
        }
    }
}

// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PromotionIdentityServer.Data;
using PromotionIdentityServer.Model;
using PromotionIdentityServer.Services;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        private ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(dbConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                opts => {
                    opts.Password.RequiredLength = 5;   // минимальная длина, 5
                    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                    opts.Password.RequireDigit = false; // требуются ли цифры
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(Configuration.GetSection("IdentityServer"))
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddExtensionGrantValidator<DelegationGrantValidator>();

            services.AddCors(options => options.AddPolicy("AnyOrigin", p => p
                                                                            .AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()
                                                                            .AllowCredentials()
                                                                            ));
            services.AddMvc();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "352027914482-cej4hjp6mruvaf3d06psjag2j1psl4kg.apps.googleusercontent.com";
                    options.ClientSecret = "hvqs-vWX0x1_W-EHuOrTZkm7";
                });

            services.AddTransient<ExternalUserProvider>();

            services.AddTransient<IExtensionGrantValidator, DelegationGrantValidator>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();

                //UserManager<ApplicationUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                // check Admin User and create, if not exists
                /*var adminUser = userManager.Users.Where(u => u.UserName == "admin").FirstOrDefault();
                if (adminUser == null)
                {
                    //admin
                    var adminUserApp = new ApplicationUser { UserName = "admin" };
                    adminUserApp.Id = "admin";
                    var result3 = userManager.CreateAsync(adminUserApp, "111111");
                }*/
            }

            app.UseDeveloperExceptionPage();
            app.UseCors("AnyOrigin");
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           /*app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();*/
        }

        /*private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();

                var um = app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();

                // check Admin User and create, if not exists
                var adminUser = um.Users.Where(u => u.UserName == "admin").FirstOrDefault();
                if (adminUser == null)
                {
                    //admin
                    var adminUserApp = new ApplicationUser { UserName = "admin" };
                    adminUserApp.Id = "admin";
                    var result3 = um.CreateAsync(adminUserApp, "111111");
                }

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }*/
    }
}

// dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
// dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
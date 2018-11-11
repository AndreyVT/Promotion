namespace PromotionServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Promotion.Application;
    using Promotion.Core.Component;
    using Promotion.DataBase;
    using Promotion.DomainWebLayer.Mappers;
    using Promotion.Server.Init;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");

            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<PromotionDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddAuthorization()
                .AddJsonFormatters();

            var identityServer = Configuration.GetSection("IdentityServer");
            var identityServerUrl = identityServer.GetValue<string>("Host");
            services.AddAuthentication("Bearer")
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = identityServerUrl;
               options.RequireHttpsMetadata = false;

               options.ApiName = "api1";
           });

            services.AddSingleton<PromotionApplication>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            // конфигурируем приложение
            serviceProvider.GetService<PromotionApplication>().ConfigureServices(services);

            services.AddCors(options => options.AddPolicy("AnyOrigin", p => p
                                                                            .AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()
                                                                            .AllowCredentials()
                                                                            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // создадим БД если нет
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<PromotionDbContext>();
                bool isDBCreated = dbContext.Database.EnsureCreated();

                if (isDBCreated)
                {
                    FirstInit startApInit = new FirstInit(dbContext);
                    startApInit.Init();
                }
            }

            // CORS
            // https://docs.asp.net/en/latest/security/cors.html
            app.UseCors(builder =>
                    builder.WithOrigins("http://localhost:4200", "*") // http://www.myclientserver.com
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            app.UseHttpsRedirection();
            
            //app.UseCors("AnyOrigin");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

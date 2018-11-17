namespace Promotion.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using Promotion.Common.Classes;
    using Promotion.Common.Interfaces;
    using Promotion.Core.Component;
    using Promotion.DataBase.Base;
    using Promotion.DataBase.Repositories;
    using Promotion.DataBase.Services;
    using Promotion.Domain.Entities;
    using Promotion.Domain.Services;
    using Promotion.DomainWebLayer.Mappers;

    public class PromotionApplication: PBaseComponent
    {
        public PromotionApplication()
        {
            
        }

        public void Configure(IServiceCollection services)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserSynchronizer, UserSynchronizer>();

            var webMappersComponent = new DomainWebLayerMappersComponent();
            webMappersComponent.ConfigureServices(services);
            // services.AddSingleton(webMappersComponent);

            AddRepositories(services);
            AddServices(services);
        }

        public override void Initialize()
        {
            
        }

        public override void PostInitialize()
        {
            
        }

        public override void PreInitialize()
        {
            
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IBaseRepository<PUser, int>, UserRepository>();
            services.AddTransient<IBaseRepository<PUserRole, int>, UserRolesRepository>();
            services.AddTransient<IBaseRepository<PRole, int>, RoleRepository>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<UserRolesService>();
            services.AddTransient<UserService>();
            services.AddTransient<RoleService>();
            services.AddTransient<UserPermissionsService>();
        }
    }
}

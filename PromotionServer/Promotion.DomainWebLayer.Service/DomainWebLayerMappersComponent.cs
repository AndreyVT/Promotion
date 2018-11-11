namespace Promotion.DomainWebLayer.Mappers
{
    using Microsoft.Extensions.DependencyInjection;
    using Promotion.Core.Component;

    public class DomainWebLayerMappersComponent : PBaseComponent
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<UserSettingsMapper>();
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
    }
}

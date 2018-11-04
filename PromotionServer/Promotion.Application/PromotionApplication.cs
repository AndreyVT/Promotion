namespace Promotion.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using Promotion.Common.Classes;
    using Promotion.Common.Interfaces;
    using Promotion.Core.Component;

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
        }
    }
}

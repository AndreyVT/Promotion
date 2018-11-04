using Microsoft.Extensions.DependencyInjection;

namespace Promotion.Core.Component
{
    public interface IBaseComponent
    {
        void ConfigureServices(IServiceCollection services);
    }
}

namespace Promotion.Core.Component
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Абстрактный класс для компонентов приложения
    /// </summary>
    public abstract class PBaseComponent: IBaseComponent
    {
        public abstract void Initialize();

        public abstract void PreInitialize();

        public abstract void PostInitialize();

        public abstract void ConfigureServices(IServiceCollection services);
    }
}

using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var modul in modules)
            {
                modul.Load(services);
            }

            return ServiceTool.Create(services);
        }
    }
}

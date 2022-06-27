using System.Diagnostics;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt.TokenValidators;

namespace Core.DependencyResolvers;

public class CoreModule:ICoreModule
{
    public void Load(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
        services.AddSingleton<TokenValidator>();
        services.AddSingleton<Stopwatch>();
    }
}
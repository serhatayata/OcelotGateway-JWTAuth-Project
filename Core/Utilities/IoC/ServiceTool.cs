using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

/*Inversion of Control(Kontrolün Tersine Çevrilmesi):  Ioc ile Uygulama içerisindeki obje instance’larının yönetimi sağlanarak,
bağımlılıklarını en aza indirgemek amaçlanmaktadır. Projeniz deki bağımlılıkların oluşturulmasını ve 
yönetilmesini geliştiricinin yerine, framework’ün yapması olarak da açıklanabilir. */
namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create( IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}

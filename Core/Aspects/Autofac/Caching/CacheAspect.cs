using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)//Default 60 dakika verdik.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            //var key = $"{methodName}({string.Join(",", arguments.Select(x=>x?.ToString()??"<Null>"))})";
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x != null ? JsonConvert.SerializeObject(x, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore}) : "<Null>"))})";

            if (_cacheManager.IsAdd(key))//Çağırılan method ve parametresi cache de varsa oradan getir.
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);



            //var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            //Type typeParameterType = typeof(IEnumerable<>);
            //if (invocation.Arguments.GetType() != typeParameterType)
            //{
            //    var arguments = invocation.Arguments;
            //    var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //    if (_cacheManager.IsAdd(key))//Çağırılan method ve parametresi cache de varsa oradan getir.
            //    {
            //        invocation.ReturnValue = _cacheManager.Get(key);
            //        return;
            //    }

            //    invocation.Proceed();
            //    _cacheManager.Add(key, invocation.ReturnValue, _duration);
            //}
            //else
            //{
            //    var arguments = invocation.Arguments.ToList();
            //    var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            //    if (_cacheManager.IsAdd(key))//Çağırılan method ve parametresi cache de varsa oradan getir.
            //    {
            //        invocation.ReturnValue = _cacheManager.Get(key);
            //        return;
            //    }

            //    invocation.Proceed();
            //    _cacheManager.Add(key, invocation.ReturnValue, _duration);
            //}
        }
    }
}

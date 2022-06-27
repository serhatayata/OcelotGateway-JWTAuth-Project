using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,System.TimeSpan.MaxValue ,TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    invocation.Proceed(); //Metodu çalıştır
                    if (invocation.ReturnValue is Task returnValueTask)
                    {
                        returnValueTask.GetAwaiter().GetResult();
                    }

                    if (invocation.ReturnValue is Task task && task.Exception != null)
                    {
                        throw task.Exception;
                    }
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();

                    throw new TransactionException(e.Message);
                }
            }
        }
    }
}

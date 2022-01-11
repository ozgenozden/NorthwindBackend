using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes=type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToString(); 
            var metotAttributes=type.GetMethod(method.Name).

        }
    }
}

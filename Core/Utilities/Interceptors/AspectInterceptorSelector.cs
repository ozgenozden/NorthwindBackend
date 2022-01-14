using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        //type ornek olarak ProductManager olabilir, method da Add metodu olabilir
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            var metotAttributes = type.GetMethod(method.Name)
            .GetCustomAttribute<MethodInterceptionBaseAttribute>(true);
            if(metotAttributes!=null )
            classAttributes.Add(metotAttributes);

            return classAttributes.OrderBy(x=>x.Priority).ToArray();

        }
        
    }
}

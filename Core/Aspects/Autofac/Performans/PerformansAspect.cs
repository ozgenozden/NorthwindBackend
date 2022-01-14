using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspects.Autofac.Performans
{
    public class PerformansAspect:MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        public PerformansAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            //geçen süre 
            if(_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance: { invocation.Method.DeclaringType.FullName}.{ invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}

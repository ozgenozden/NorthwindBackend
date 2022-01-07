﻿using Castle.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //bu attribute clasların en tepesinde kullanılır,metotlarda kullanılabilir,arzu edilirse 1 den fazla kullanılabilir,inherit edilen alt claslardada kullanılabilir.
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {
            
        }
    }
}

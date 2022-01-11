using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //bu clasda bir metodu nasıl yorumlayacağını anlattığımız yer olacaktır.
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        //metodun çalışmasının önünde
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        //bu kodu nasıl yorumlayacağımızı anlatacağız.
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                //operasyonu çalıştır demek
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;  
                OnException(invocation);
                throw;
            }
            finally
            {
                if(isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}

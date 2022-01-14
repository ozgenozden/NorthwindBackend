using Castle.DynamicProxy;
using Core.CrossCuttingConcerts.Loging;
using Core.CrossCuttingConcerts.Loging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Logging
{
    public  class LogAspect:MethodInterception
    {
        LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if(loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase =(LoggerServiceBase) Activator.CreateInstance(loggerService);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info();
        }
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = invocation.Arguments.Select(x => new LogParameter
            {
                Value=x.ToString(),
                Type=x.GetType().Name
            }).ToList();

            var logDetail = new LogDetail();

        }
    }
}

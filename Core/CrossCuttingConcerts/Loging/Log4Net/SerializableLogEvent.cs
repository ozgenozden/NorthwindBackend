using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerts.Loging.Log4Net
{
    

    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _logingEvent;
        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _logingEvent=loggingEvent;
        }
        public object Message => _logingEvent.MessageObject;
    }
}

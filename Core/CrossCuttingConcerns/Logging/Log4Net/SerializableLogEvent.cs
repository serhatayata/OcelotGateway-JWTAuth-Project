using System;
using log4net.Core;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        //Buraya bu işlemi kimin yaptığına dair user bilgisi de eklenebilir.
        public string DeviceName => _loggingEvent.UserName;//Bilgisayar veya Cihaz adı.
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public string Date => DateTime.Now.ToString("yyyyMMddHHmmssfff");
        public object Message => _loggingEvent.MessageObject;
    }
}

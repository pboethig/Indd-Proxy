using System;
using System.Diagnostics;


namespace Indd.Service.Log
{
    /// <summary>
    /// Syslogwrapper
    /// </summary>
    public class Syslog
    {
        /// <summary>
        /// Logs in syslog
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="logfileName"></param>
        public static void log(string logEvent, EventLogEntryType type = EventLogEntryType.Error, int code=234, string logfileName="InddProxy")
        {
            string source = "InddProxy";
           
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, logfileName);
             
                return;
            }

            EventLog log = new EventLog();

            log.Source = source;
 
            log.WriteEntry(logEvent, type, code);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Log
    {
        /// <summary>
        /// This is our Logging class. Which logs the servers actions.
        /// </summary>
        /// <param name="LogInfo"></param>
        public static void WriteInfo(string LogInfo)
        {
            string sSource;
            string sLog;
            
            sSource = "Mubeen Adel Server";
            sLog = "Application";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, LogInfo, EventLogEntryType.Information);
        }
    }
}
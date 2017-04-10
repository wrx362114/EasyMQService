using Topshelf.Logging;
using System.Diagnostics;

namespace ES.Framework.Core
{
    public class Logger
    {
        private LogWriter Log;
        public Logger(LogWriter log)
        {
            Log = log;
        }
        public void WriteLog(SourceLevels lv, string formart, params string[] args)
        { 
            Log.LogFormat(LoggingLevel.FromSourceLevels(lv), formart, args);
        }
    }
    public static class LoggerExtend
    { 
        public static void Info(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Information, info, args);
        } 
        public static void Debug(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Verbose, info, args);
        } 
        public static void Error(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Error, info, args);
        } 
        public static void Fatal(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Critical, info, args);
        } 
        public static void None(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Off, info, args);
        }
        public static void Warn(this Logger log, string info, params string[] args)
        {
            log.WriteLog(SourceLevels.Warning, info, args);
        }

    }
}

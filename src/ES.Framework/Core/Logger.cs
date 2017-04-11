using Topshelf.Logging;
using System.Diagnostics;
using Hangfire.Logging;
using System;

namespace ES.Framework.Core
{
    public class Logger : ILog
    {
        private LogWriter logger;
        public Logger(LogWriter log)
        {
            logger = log;
        }
        public void WriteLog(SourceLevels lv, string formart, params string[] args)
        {
            if (args == null || args.Length == 0)
            {
                logger.Log(LoggingLevel.FromSourceLevels(lv), formart);
            }
            else
            {
                logger.LogFormat(LoggingLevel.FromSourceLevels(lv), formart, args);
            }
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception)
        {
            if (messageFunc == null)
            {
                return false;
            }
            WriteLog(logLevel.GetSourceLevels(), messageFunc() + (exception != null ? exception.ToString() : ""));
            return true;
        }
    }
    public class LogProvider : ILogProvider
    {
        public LogProvider(Logger log)
        {
            logger = log;
        }
        private Logger logger;
        public ILog GetLogger(string name)
        {
            return logger;
        }
    }
    public static class LoggerExtend
    {
        public static SourceLevels GetSourceLevels(this LogLevel olv)
        {
            switch (olv)
            {
                case LogLevel.Trace:
                    return SourceLevels.ActivityTracing;
                case LogLevel.Debug:
                    return SourceLevels.Verbose;
                case LogLevel.Info:
                    return SourceLevels.Information;
                case LogLevel.Warn:
                    return SourceLevels.Warning;
                case LogLevel.Error:
                    return SourceLevels.Error;
                case LogLevel.Fatal:
                    return SourceLevels.Critical;
                default:
                    return SourceLevels.Off;
            }
        }
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

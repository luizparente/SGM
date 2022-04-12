using Orion.Utilities.Configuration;
using Orion.Utilities.Logger.Interfaces;
using Serilog;
using System;

namespace Orion.Utilities.Logger {
    /// <summary>
    /// A logger that registers occurrences to a file that follows specifications defined in orionsettings.json.
    /// </summary>
    public class FileLogger : IFileLogger {
        private Serilog.Core.Logger logger;

        public FileLogger() {
            string logPath;
            string fileName;

            try {
                logPath = ConfigurationManager.Settings["LogFolder"];

                if (!logPath.EndsWith('/'))
                    logPath += "/";

                if (string.IsNullOrWhiteSpace(logPath))
                    throw new Exception("Path not found in configuration file settings.json.");
            }
            catch {
                logPath = "logs/";
            }

            try {
                fileName = ConfigurationManager.Settings["LogFileName"];

                if (string.IsNullOrWhiteSpace(logPath))
                    throw new Exception("Log file name not found in configuration file settings.json.");
            }
            catch {
                fileName = "LG.log";
            }

            this.logger = new LoggerConfiguration()
                            .WriteTo
                            .File($"{logPath}{fileName}", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff}   {Message:lj}{NewLine}{Exception}")
                            .CreateLogger();
        }

        public void Log(string message, Severity? severity = null, Exception exception = null) {
            severity = severity ?? Severity.Informational;
            string typeString;
            Serilog.Events.LogEventLevel level;

            switch (severity) {
                case Severity.Error:
                    level = Serilog.Events.LogEventLevel.Error;
                    typeString = "ERROR: ";
                    break;
                case Severity.Warning:
                    level = Serilog.Events.LogEventLevel.Warning;
                    typeString = "WARNING: ";
                    break;
                case Severity.Fatal:
                    level = Serilog.Events.LogEventLevel.Fatal;
                    typeString = "FATAL: ";
                    break;
                case Severity.Informational:
                    level = Serilog.Events.LogEventLevel.Information;
                    typeString = "INFO: ";
                    break;
                case Severity.Debug:
                    level = Serilog.Events.LogEventLevel.Debug;
                    typeString = "DEBUG: ";
                    break;
                default:
                    level = Serilog.Events.LogEventLevel.Information;
                    typeString = "INFO: ";
                    break;
            }

            if (exception != null)
                if (exception.InnerException != null)
                    this.logger.Write(level, $"{typeString}{message}\n{exception.Message}\n{exception.InnerException.Message}");
                else
                    this.logger.Write(level, $"{typeString}{message}\n{exception.Message}");
            else
                this.logger.Write(level, $"{typeString}{message}");
        }
    }
}

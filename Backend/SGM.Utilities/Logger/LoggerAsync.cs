using Orion.Utilities.Configuration;
using Orion.Utilities.Logger.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Orion.Utilities.Logger {
    /// <summary>
    /// A logger that asynchronously writes logs to the file system, in the directory specified in orionsettings.json.
    /// Note: Log path is defined in orionsettings.json. Log name follows format yyyyMMdd_HHmmssffff.log.
    /// </summary>
    public class LoggerAsync : ILoggerAsync {
        public async Task LogAsync(string message) {
            string filename = this.GetLogFileName();
            string log = this.FormatLogMessage(message, null, null);
            string folder = this.GetLogDirectory();

            await this.LogToDisk(folder, filename, log);
        }

        public async Task LogAsync(string message, Severity severity) {
            string filename = this.GetLogFileName();
            string log = this.FormatLogMessage(message, severity, null);
            string folder = this.GetLogDirectory();

            await this.LogToDisk(folder, filename, log);
        }

        public async Task LogAsync(Exception exception) {
            string filename = this.GetLogFileName();
            string log = this.FormatLogMessage(null, Severity.Error, exception);
            string folder = this.GetLogDirectory();

            await this.LogToDisk(folder, filename, log);
        }

        public async Task LogAsync(Exception exception, Severity severity) {
            string filename = this.GetLogFileName();
            string log = this.FormatLogMessage(null, severity, exception);
            string folder = this.GetLogDirectory();

            await this.LogToDisk(folder, filename, log);
        }

        public async Task LogAsync(string message, Severity severity, Exception exception) {
            string filename = this.GetLogFileName();
            string log = this.FormatLogMessage(message, severity, exception);
            string folder = this.GetLogDirectory();

            await this.LogToDisk(folder, filename, log);
        }

        private string FormatLogMessage(string message, Severity? severity = null, Exception exception = null) {
            severity = severity ?? Severity.Informational;
            string flag;
            string exceptionString = string.Empty;

            switch (severity) {
                case Severity.Warning:
                    flag = "WARNING";
                    break;
                case Severity.Error:
                    flag = "ERROR";
                    break;
                case Severity.Fatal:
                    flag = "FATAL ERROR";
                    break;
                case Severity.Debug:
                    flag = "DEBUG";
                    break;
                default:
                    flag = "INFO";
                    break;
            }

            if (exception != null)
                if (exception.InnerException != null)
                    exceptionString = $"{exception.Message} {exception.InnerException.Message}";
                else
                    exceptionString = $"{exception.Message}";

            return $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} {flag}: {message} {exceptionString}";
        }

        private string GetLogFileName() {
            var now = DateTime.Now;

            return $"{now:yyyyMMdd_HHmmssffff}.log";
        }

        private string GetLogDirectory() {
            string path;

            try { 
                path = ConfigurationManager.Settings["LogFolder"];

                if (string.IsNullOrWhiteSpace(path))
                    throw new Exception("Path not found in configuration file settings.json.");
            }
            catch {
                path = "/logs/";
            }

            path.Replace('\\', '/');

            if (!path.StartsWith('/'))
                path = "/" + path;

            if (!path.EndsWith('/'))
                path += "/";

            return AppDomain.CurrentDomain.BaseDirectory + path;
        }

        private async Task LogToDisk(string path, string filename, string log) {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(filename))
                return;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var writer = new StreamWriter(path + filename, true)) {
                await writer.WriteLineAsync(log);
            }
        }
    }
}

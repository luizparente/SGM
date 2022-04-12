using Orion.Utilities.Configuration;
using Orion.Utilities.Database.Mapper;
using Orion.Utilities.Logger.Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Utilities.Logger {
    /// <summary>
    /// This class is a concrete implementation of IDatabaseLogger.
    /// It defines the concrete logic for a logger that writes entries to a database table defined in orionsettings.json.
    /// In order to use this logger, a database table with the same name must exist in the database, and a connection string must also be defined in orionsettings.json.
    /// Note: Log path is defined in orionsettings.json. Log name follows format LGhhmmss.julianday.
    /// </summary>
    public class DatabaseLogger : IDatabaseLoggerAsync {
        protected readonly string connectionString;
        protected readonly string logTable;
        protected readonly string outputPath;

        public DatabaseLogger() {
            this.connectionString = ConfigurationManager.Settings["ConnectionString"];
            this.logTable = ConfigurationManager.Settings["LogTable"];
            this.outputPath = ConfigurationManager.Settings["LogFolder"];

            if (string.IsNullOrWhiteSpace(logTable))
                this.logTable = "Log";

            Mapper.CreateTableAsync<LogEntry>(this.connectionString, this.logTable);
        }

        public async Task ExportToFileAsync(string identifier, DateTime day) {
            day = day.ToUniversalTime();
            var builder = new StringBuilder();
            string sql = $@"SELECT  *
                            FROM    {this.logTable}
                            WHERE   Identifier = '{identifier}'
                                    AND CONVERT(NVARCHAR, TimestampUTC, 23) LIKE '{day.Year:0000}-{day.Month:00}-{day.Day:00}%'
                            ORDER BY TimestampUTC";
            var entries = await Mapper.GetResultsAsync<LogEntry>(sql, this.connectionString);

            foreach (var entry in entries)
                builder.AppendLine($"{entry.TimestampUTC.ToString("MM-dd-yyyy hh:mm:ss tt")} {entry.Severity}: {entry.Message} {(!string.IsNullOrWhiteSpace(entry.Exception) ? $"- {entry.Exception}" : "")} {(!string.IsNullOrWhiteSpace(entry.InnerException) ? $"- {entry.InnerException}" : "")} ");

            string filename = this.GetLogFileName();

            await this.SaveToDiskAsync(this.outputPath, filename, builder.ToString());
        }

        public async Task LogAsync(string identifier, string message) {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(message))
                throw new Exception("Entry identifier or message was null.");

            var entry = new LogEntry() {
                Identifier = identifier,
                Message = message,
                Severity = this.ConvertSeverity(Severity.Informational),
                TimestampUTC = DateTime.UtcNow
            };

            await Mapper.WriteAsync(entry, this.connectionString, this.logTable);
        }

        public async Task LogAsync(string identifier, string message, Severity severity) {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(message))
                throw new Exception("Entry identifier or message was null.");

            var entry = new LogEntry() {
                Identifier = identifier,
                Message = message,
                Severity = this.ConvertSeverity(severity),
                TimestampUTC = DateTime.UtcNow
            };

            await Mapper.WriteAsync(entry, this.connectionString, this.logTable);
        }

        public async Task LogAsync(string identifier, string message, Severity severity, Exception exception) {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(message) || exception == null)
                throw new Exception("Entry identifier, message or exception object was null.");

            var entry = new LogEntry() {
                Identifier = identifier,
                Message = message,
                Severity = this.ConvertSeverity(severity),
                Exception = exception.Message,
                InnerException = exception.InnerException?.Message,
                TimestampUTC = DateTime.UtcNow
            };

            await Mapper.WriteAsync(entry, this.connectionString, this.logTable);
        }

        public async Task LogAsync(string identifier, Exception exception) {
            if (string.IsNullOrWhiteSpace(identifier) || exception == null)
                throw new Exception("Entry identifier or exception object was null.");

            var entry = new LogEntry() {
                Identifier = identifier,
                Message = exception.Message,
                Severity = this.ConvertSeverity(Severity.Error),
                Exception = exception.Message,
                InnerException = exception.InnerException?.Message,
                TimestampUTC = DateTime.UtcNow
            };

            await Mapper.WriteAsync(entry, this.connectionString, this.logTable);
        }

        public async Task LogAsync(string identifier, Exception exception, Severity severity) {
            if (string.IsNullOrWhiteSpace(identifier) || exception == null)
                throw new Exception("Entry identifier or exception object was null.");

            var entry = new LogEntry() {
                Identifier = identifier,
                Message = exception.Message,
                Severity = this.ConvertSeverity(severity),
                Exception = exception.Message,
                InnerException = exception.InnerException?.Message,
                TimestampUTC = DateTime.UtcNow
            };

            await Mapper.WriteAsync(entry, this.connectionString, this.logTable);
        }

        private string GetLogFileName() {
            int julian = new JulianCalendar().GetDayOfYear(DateTime.Now);
            var now = DateTime.Now;

            return $"LG{now.Hour:00}{now.Minute:00}{now.Second:00}.{julian:000}";
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

        private async Task SaveToDiskAsync(string path, string filename, string log) {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(filename))
                return;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var writer = new StreamWriter(path + filename, true)) {
                await writer.WriteLineAsync(log);
            }
        }

        private string ConvertSeverity(Severity severity) {
            string flag;

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
                case Severity.Critical:
                    flag = "CRITICAL ERROR";
                    break;
                case Severity.ActionRequired:
                    flag = "ACTION REQUIRED";
                    break;
                case Severity.Debug:
                    flag = "DEBUG";
                    break;
                default:
                    flag = "INFO";
                    break;
            }

            return flag;
        }

        public async Task LogAndExportToFileAsync(string identifier, string message, Exception exception = null, Severity? severity = null) {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(message) && exception == null)
                throw new Exception("A log identifier, and a message or exception are necessary.");

            Severity flag = severity ?? Severity.Informational;

            if (severity == null && exception != null)
                flag = Severity.Error;

            if (!string.IsNullOrWhiteSpace(message)) {
                if (exception != null)
                    await this.LogAsync(identifier, message, flag, exception);
                else
                    await this.LogAsync(identifier, message, flag);
            }
            else {
                await this.LogAsync(identifier, exception, flag);
            }

            await this.ExportToFileAsync(identifier, DateTime.UtcNow);
        }

        private class LogEntry {
            public string EntryGuid { get; set; }
            public string Identifier { get; set; }
            public DateTime TimestampUTC { get; set; }
            public string Severity { get; set; }
            public string Message { get; set; }
            public string Exception { get; set; }
            public string InnerException { get; set; }

            public LogEntry() {
                this.EntryGuid = Guid.NewGuid().ToString();
            }
        }
    }
}

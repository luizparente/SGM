using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Utilities.Logger.Interfaces {
    /// <summary>
    /// An interface that defines an asynchronous logger that will write entries to a database.
    /// </summary>
    public interface IDatabaseLoggerAsync {
        /// <summary>
        /// Asynchronously exports log entries from the database to a file.
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="day">The day for which to retrieve logs. Note: Log timestamps are converted to UTC when writing to the database.</param>
        /// <param name="path">The desired output path.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task ExportToFileAsync(string identifier, DateTime day);

        /// <summary>
        /// Logs a message with severity level "Informational".
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="message">The message to be logged.</param>
        /// <returns></returns>
        public Task LogAsync(string identifier, string message);

        /// <summary>
        /// Logs a message with the specified severity level.
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="message">The message to be logged.</param>
        /// <param name="severity">The severity level.</param>
        /// <returns></returns>
        public Task LogAsync(string identifier, string message, Severity severity);

        /// <summary>
        /// Logs a message with the specified severity level, followed by a thrown Exception.
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="message">The message to be logged.</param>
        /// <param name="severity">The severity level.</param>
        /// <param name="exception">The Exception thrown.</param>
        /// <returns></returns>
        public Task LogAsync(string identifier, string message, Severity severity, Exception exception);

        /// <summary>
        /// Logs an Exception with severity level "Error".
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="exception">The Exception thrown.</param>
        /// <returns></returns>
        public Task LogAsync(string identifier, Exception exception);

        /// <summary>
        /// Logs an Exception with the specified severity level.
        /// </summary>
        /// <param name="identifier">The identifier through which to find a group of entries.</param>
        /// <param name="exception">The Exception thrown.</param>
        /// <param name="severity">The severity level.</param>
        /// <returns></returns>
        public Task LogAsync(string identifier, Exception exception, Severity severity);

        /// <summary>
        /// Asynchronously creates a log entry and automatically exports it to a file based on the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier through which to fetch and group log entries by.</param>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">The exception being thrown (if any).</param>
        /// <param name="severity">The log severity.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task LogAndExportToFileAsync(string identifier, string message, Exception exception = null, Severity? severity = null);
    }
}

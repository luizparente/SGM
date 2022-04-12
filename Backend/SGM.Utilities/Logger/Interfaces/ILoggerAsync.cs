using System;
using System.Threading.Tasks;

namespace Orion.Utilities.Logger.Interfaces {
    /// <summary>
    /// An Asynchronous logger.
    /// </summary>
    public interface ILoggerAsync {
        /// <summary>
        /// Logs a message with severity level "Informational".
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <returns></returns>
        public Task LogAsync(string message);

        /// <summary>
        /// Logs a message with the specified severity level.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="severity">The severity level.</param>
        /// <returns></returns>
        public Task LogAsync(string message, Severity severity);

        /// <summary>
        /// Logs a message with the specified severity level, followed by a thrown Exception.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="severity">The severity level.</param>
        /// <param name="exception">The Exception thrown.</param>
        /// <returns></returns>
        public Task LogAsync(string message, Severity severity, Exception exception);

        /// <summary>
        /// Logs an Exception with severity level "Error".
        /// </summary>
        /// <param name="exception">The Exception thrown.</param>
        /// <returns></returns>
        public Task LogAsync(Exception exception);

        /// <summary>
        /// Logs an Exception with the specified severity level.
        /// </summary>
        /// <param name="exception">The Exception thrown.</param>
        /// <param name="severity">The severity level.</param>
        /// <returns></returns>
        public Task LogAsync(Exception exception, Severity severity);
    }
}

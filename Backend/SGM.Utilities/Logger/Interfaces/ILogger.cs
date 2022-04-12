using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Utilities.Logger.Interfaces {
    /// <summary>
    /// A synchronous logger.
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// Logs the entry with the specified message, severity and exception thrown.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="severity">The severity of the occurence.</param>
        /// <param name="exception">The exception thrown.</param>
        public void Log(string message, Severity? severity = null, Exception exception = null);
    }
}

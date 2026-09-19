using System;
using System.Diagnostics;
using System.Text;

namespace Salem.Utils {
    /// <summary>
    /// Provides static methods for logging information, warning, and error events to the Windows event log.
    /// </summary>
    /// <remarks>Not thread-safe due to race conditions.</remarks>
    public static class EventLogger {
        private static string _sourceName = null;

        /// <summary>
        /// Configures the event log source with the specified source and log names.
        /// </summary>
        /// <param name="sourceName">The name of the event log source to create or use.</param>
        /// <param name="logName">The name of the event log to associate with the source.</param>
        /// <exception cref="ArgumentException">Thrown when sourceName or logName is null or empty.</exception>
        /// <remarks>Concurrent calls of this method is not thread-safe.</remarks>
        public static void Configure(string sourceName, string logName) {
            if (string.IsNullOrWhiteSpace(sourceName) || string.IsNullOrWhiteSpace(logName))
                throw new ArgumentException($"The values for '{nameof(sourceName)}' and '{nameof(logName)}' must not be null or empty.");

            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, logName);

            _sourceName = sourceName;
        }

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Information"/> type entry, with the given message text, to the event log.
        /// </summary>
        /// <param name="message">The message to be logged in the event log.</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        /// <exception cref="InvalidOperationException">The value of <see cref="_sourceName"/> is not configured.</exception>
        public static void LogInformation(string message, int eventID = 0) => LogEntry(message, EventLogEntryType.Information, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Warning"/> type entry, with the given message text, to the event log.
        /// </summary>
        /// <param name="message">The message to be logged in the event log.</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        /// <exception cref="InvalidOperationException">The value of <see cref="_sourceName"/> is not configured.</exception>
        public static void LogWarning(string message, int eventID = 0) => LogEntry(message, EventLogEntryType.Warning, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry, with the given message text, to the event log.
        /// </summary>
        /// <param name="message">The message to be logged in the event log.</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        /// <exception cref="InvalidOperationException">The value of <see cref="_sourceName"/> is not configured.</exception>
        public static void LogError(string message, int eventID = 0) => LogEntry(message, EventLogEntryType.Error, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry message based on the exception provided to the event log.
        /// </summary>
        /// <param name="ex">The exception on which the message to be logged in the event log is based on.</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        /// <exception cref="InvalidOperationException">The value of <see cref="_sourceName"/> is not configured.</exception>
        public static void LogError(Exception ex, int eventID = 0) => LogEntry(FormatExceptionForLogging(null, ex), EventLogEntryType.Error, eventID);

        /// <summary>
        /// Writes an <see cref="EventLogEntryType.Error"/> type entry from both the message and exception provided to the event log.
        /// </summary>
        /// <param name="message">The message to be logged in the event log.</param>
        /// <param name="ex">The exception on which the message to be logged in the event log is based on.</param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        /// <exception cref="InvalidOperationException">The value of <see cref="_sourceName"/> is not configured.</exception>
        public static void LogError(string message, Exception ex, int eventID = 0) => LogEntry(FormatExceptionForLogging(message, ex), EventLogEntryType.Error, eventID);

        private static string FormatExceptionForLogging(string message, Exception ex) {
            if (string.IsNullOrWhiteSpace(message) && ex is null)
                return "[No data available]";

            var sb = new StringBuilder();

            if (ex != null) {
                sb.AppendLine("=== Exception ===");
                sb.AppendLine(ex.ToString());
            }

            if (!string.IsNullOrWhiteSpace(message)) {
                sb.AppendLine("\n=== Developer Message ===");
                sb.AppendLine(message);
            }

            return sb.ToString();
        }

        private static string GetSourceName() => _sourceName ?? throw new InvalidOperationException($"The value of '{nameof(_sourceName)}' is not configured.");

        private static void LogEntry(string message, EventLogEntryType entryType, int eventID) => EventLog.WriteEntry(GetSourceName(), string.IsNullOrWhiteSpace(message) ? "[No data available]" : message, entryType, eventID);
    }
}

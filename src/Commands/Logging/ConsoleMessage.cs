using Microsoft.Extensions.Logging;

namespace Commands.Logging
{
	/// <summary>
	/// Represents a single log entry produced by <see cref="ConsoleLogger"/> and captured for inspection or output.
	/// </summary>
	public class ConsoleMessage
	{
		/// <summary>
		/// Gets the severity level of the log message.
		/// </summary>
		public LogLevel LogLevel { get; internal set; }

		/// <summary>
		/// Gets the <see cref="EventId"/> associated with the log message, if any.
		/// </summary>
		public EventId EventId { get; internal set; }

		/// <summary>
		/// Gets the formatted log message text.
		/// </summary>
		public string? Message { get; internal set; }

		/// <summary>
		/// Gets an optional <see cref="Exception"/> associated with the log entry.
		/// </summary>
		public Exception? Exception { get; internal set; }
	}
}
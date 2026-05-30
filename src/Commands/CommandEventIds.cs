using Microsoft.Extensions.Logging;

namespace Commands
{
	/// <summary>
	/// Provides well-known <see cref="EventId"/> values used for command-related logging.
	/// </summary>
	/// <remarks>
	/// Each <see cref="EventId"/> is assigned a stable numeric identifier and a short name that can be used
	/// by logging frameworks and listeners to categorize command lifecycle events and errors.
	/// </remarks>
	public static class CommandEventIds
	{
		/// <summary>
		/// Gets the event id used for general debug statements.
		/// </summary>
		public static EventId Debug => new EventId(1000, "Debug statement");

		/// <summary>
		/// Gets the event id used when command validation fails.
		/// </summary>
		public static EventId ValidationErrorOcurred => new EventId(1001, "ValidationErrorOccurred");

		/// <summary>
		/// Gets the event id raised when command execution starts.
		/// </summary>
		public static EventId CommandExecutionStarted => new EventId(1002, "CommandExecutionStarted");

		/// <summary>
		/// Gets the event id raised when command execution completes successfully.
		/// </summary>
		public static EventId CommandExecutionCompleted => new EventId(1003, "CommandExecutionCompleted");

		/// <summary>
		/// Gets the event id raised when command execution fails with an error.
		/// </summary>
		public static EventId CommandExecutionFailed => new EventId(1004, "CommandExecutionFailed");

		/// <summary>
		/// Gets the event id used when no command was provided to an execution API.
		/// </summary>
		public static EventId CommandNotProvided => new EventId(1005, "CommandNotProvided");

		/// <summary>
		/// Gets the event id used when a command cannot be executed in the current context.
		/// </summary>
		public static EventId CommandCannotExecute => new EventId(1006, "CommandCannotExecute");

		/// <summary>
		/// Gets the event id used when a command completes with an informational message.
		/// </summary>
		public static EventId CommandCompletedWithMessage => new EventId(1007, "CommandCompletedWithMessage");

		/// <summary>
		/// Gets the event id used for unexpected execution errors that occurred during command processing.
		/// </summary>
		public static EventId CommandExecutionError => new EventId(1008, "CommandExecutionError");

	}
}

using Microsoft.Extensions.Logging;

namespace Commands
{
	public static class CommandEventIds
	{
		public static EventId Debug => new(1000, "Debug statement");
		public static EventId ValidationErrorOcurred => new(1001, "ValidationErrorOccurred");
		public static EventId CommandExecutionStarted => new(1002, "CommandExecutionStarted");
		public static EventId CommandExecutionCompleted => new(1003, "CommandExecutionCompleted");
		public static EventId CommandExecutionFailed => new(1004, "CommandExecutionFailed");
		public static EventId CommandNotProvided => new(1005, "CommandNotProvided");
		public static EventId CommandCannotExecute => new(1006, "CommandCannotExecute");
		public static EventId CommandCompletedWithMessage => new(1007, "CommandCompletedWithMessage");
		public static EventId CommandExecutionError => new(1008, "CommandExecutionError");

	}
}

using Microsoft.Extensions.Logging;

namespace Commands.Logging
{
	public class ConsoleMessage
	{
		public LogLevel LogLevel { get; internal set; }
		public EventId EventId { get; internal set; }
		public string? Message { get; internal set; }
		public Exception? Exception { get; internal set; }
	}
}
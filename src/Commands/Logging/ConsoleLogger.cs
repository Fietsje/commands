using System.Text;
using Microsoft.Extensions.Logging;

namespace Commands.Logging
{
	public class ConsoleLogger(LogLevel currentLogLevel) : ILogger
	{
		private readonly IList<ConsoleLoggerScopeItem> _scopes = [];

		public ICollection<ConsoleMessage> Messages { get; } = [];


		public IDisposable? BeginScope<TState>(TState state) where TState : notnull
		{
			ConsoleLoggerScopeItem? item = new();
			ConsoleLoggerScope scope = new ConsoleLoggerScope(state, () => _scopes.Remove(item));
			item.Scope = scope;
			_scopes.Add(item);
			return scope;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel >= currentLogLevel;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			StringBuilder builder = new();
			builder.Append($"[{logLevel}] {(eventId.Name != null ? eventId.Name : eventId.Id)} - {formatter(state, exception)}");
			if (exception != null)
			{
				builder.Append($" Exception: {exception.Message}");
				if (exception.StackTrace != null)
				{
					builder.AppendLine();
					builder.Append(exception.StackTrace);
				}
			}
			foreach (var scope in _scopes) { builder.Append($" [{scope.Scope!.State}]"); }

			Messages.Add(new ConsoleMessage
			{
				LogLevel = logLevel,
				EventId = eventId,
				Message = builder.ToString(),
				Exception = exception
			});
			Console.WriteLine(builder.ToString());
		}
	}
}

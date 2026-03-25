using System.Text;
using Microsoft.Extensions.Logging;

namespace Commands.Logging
{
	/// <summary>
	/// A lightweight console-backed <see cref="ILogger"/> implementation that writes formatted log messages
	/// to the console and stores them in the <see cref="Messages"/> collection for inspection.
	/// </summary>
	/// <param name="currentLogLevel">The minimum <see cref="LogLevel"/> that will be recorded and written to the console.</param>
	public class ConsoleLogger(LogLevel currentLogLevel) : ILogger
	{
		private readonly IList<ConsoleLoggerScopeItem> _scopes = [];

		/// <summary>
		/// Gets a collection of <see cref="ConsoleMessage"/> entries captured by this logger.
		/// </summary>
		public ICollection<ConsoleMessage> Messages { get; } = [];


		/// <summary>
		/// Begins a logical operation scope.
		/// </summary>
		/// <typeparam name="TState">The type of the state to associate with the scope. Must be non-null.</typeparam>
		/// <param name="state">The state to associate with the started scope.</param>
		/// <returns>
		/// An <see cref="IDisposable"/> that ends the scope when disposed, or <c>null</c> if scope support is not available.
		/// </returns>
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull
		{
			ConsoleLoggerScopeItem? item = new();
			ConsoleLoggerScope scope = new ConsoleLoggerScope(state, () => _scopes.Remove(item));
			item.Scope = scope;
			_scopes.Add(item);
			return scope;
		}

		/// <summary>
		/// Determines whether the specified <paramref name="logLevel"/> is enabled for this logger.
		/// </summary>
		/// <param name="logLevel">The <see cref="LogLevel"/> to check.</param>
		/// <returns><c>true</c> if the provided <paramref name="logLevel"/> is greater than or equal to the configured minimum; otherwise <c>false</c>.</returns>
		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel >= currentLogLevel;
		}

		/// <summary>
		/// Writes a log entry.
		/// </summary>
		/// <typeparam name="TState">The type of the <paramref name="state"/> object.</typeparam>
		/// <param name="logLevel">The severity level of the log.</param>
		/// <param name="eventId">An optional event id that identifies the event.</param>
		/// <param name="state">The state associated with the log entry.</param>
		/// <param name="exception">An optional <see cref="Exception"/> related to the log entry.</param>
		/// <param name="formatter">A function that creates the formatted message string from the <paramref name="state"/> and <paramref name="exception"/>.</param>
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

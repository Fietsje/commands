using Commands.Logging;
using Microsoft.Extensions.Logging;

namespace Commands.Diagnostics
{
	/// <summary>
	/// Provides a diagnostic implementation of <see cref="ICommandContext"/> that
	/// executes commands and exposes basic diagnostic logging and disposal callbacks.
	/// </summary>
	public class DiagnosticCommandContext : ICommandContext
	{
		private bool disposedValue;
		private readonly ICommandExecutor executor;

		/// <summary>
		/// Optional callback invoked once the context has been disposed.
		/// </summary>
		public Action? OnDisposed { get; set; }

		/// <summary>
		/// The logger used by the context. If <c>null</c> is passed to the constructor
		/// a <see cref="ConsoleLogger"/> with <see cref="LogLevel.Trace"/> is used as fallback.
		/// </summary>
		public ILogger Logger { get; init; }

		/// <summary>
		/// Indicates whether the context has been disposed.
		/// </summary>
		public bool IsDisposed { get; }

		/// <summary>
		/// The command resolver used to locate or build command instances.
		/// </summary>
		public ICommandResolver CommandResolver { get; init; }

		/// <summary>
		/// Initializes a new instance of <see cref="DiagnosticCommandContext"/>.
		/// </summary>
		/// <param name="logger">The logger to use. If <c>null</c> a default <see cref="ConsoleLogger"/> is used.</param>
		/// <param name="resolver">The command resolver. If <c>null</c> a <see cref="DiagnosticCommandResolver"/> is used.</param>
		/// <param name="executor">The command executor. If <c>null</c> a <see cref="DiagnosticCommandExecutor"/> is created using <paramref name="logger"/>.</param>
		public DiagnosticCommandContext(ILogger logger, ICommandResolver resolver, ICommandExecutor executor)
		{
			Logger = logger ?? new ConsoleLogger(LogLevel.Trace);
			CommandResolver = resolver ?? new DiagnosticCommandResolver();
			this.executor = executor ?? new DiagnosticCommandExecutor(Logger);
		}

		/// <summary>
		/// Executes the provided command using the configured <see cref="ICommandExecutor"/>.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type that implements <see cref="ICommand"/>.</typeparam>
		/// <param name="command">The command instance to execute. May be <c>null</c>.</param>
		/// <returns>The same command instance that was passed in. Returns <c>null</c> if <paramref name="command"/> was <c>null</c>.</returns>
		public virtual TCommand? Execute<TCommand>(TCommand? command)
			where TCommand : class, ICommand
		{

			if (command is null)
			{
				Logger?.LogWarning(CommandEventIds.CommandNotProvided, "No command provided for execution.");
			}
			else
			{
				executor?.Execute(this, command);
			}

			return command;
		}


		/// <summary>
		/// Releases resources used by the context.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}

			OnDisposed?.Invoke();
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~DiagnosticCommandContext()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

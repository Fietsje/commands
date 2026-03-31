using Commands.Logging;
using Microsoft.Extensions.Logging;

namespace Commands
{
	public class CommandContext : ICommandContext
	{
		private bool disposedValue;
		private readonly ICommandExecutor executor;

		public ILogger Logger { get; init; }
		public bool IsDisposed { get; private set; }
		public ICommandResolver CommandResolver { get; init; }

		public CommandContext(ILogger? logger = null, ICommandResolver? resolver = null, ICommandExecutor? executor = null)
		{
			Logger = logger ?? new ConsoleLogger(LogLevel.Trace);
			CommandResolver = resolver ?? new CommandResolver();
			this.executor = executor ?? new CommandExecutor(this, Logger);
		}

		public virtual TCommand? Execute<TCommand>(TCommand? command)
			where TCommand : class, ICommand
		{

			if (command is null)
			{
				Logger?.LogWarning(CommandEventIds.CommandNotProvided, "No command provided for execution.");
			}
			else
			{
				executor.Execute(command);
			}

			return command;
		}

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
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~CommandContext()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

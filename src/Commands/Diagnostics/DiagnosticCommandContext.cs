using Microsoft.Extensions.Logging;

namespace Commands.Diagnostics
{
	public class DiagnosticCommandContext(ICommandContext commandContext, ICommandExecutor commandExecutor) : ICommandContext
	{
		private bool disposedValue;
		public Action? OnDisposed { get; set; }

		public ILogger Logger => commandContext.Logger;
		public bool IsDisposed { get; }
		public ICommandResolver CommandResolver => commandContext.CommandResolver;

		public virtual TCommand? Execute<TCommand>(TCommand? command)
			where TCommand : class, ICommand
		{
			ArgumentNullException.ThrowIfNull(commandContext, nameof(commandContext));
			if (commandContext.IsDisposed)
			{
				throw new ObjectDisposedException(nameof(CommandContext), "Cannot execute command on a disposed context.");
			}

			if (command is null)
			{
				Logger?.LogWarning(CommandEventIds.CommandNotProvided, "No command provided for execution.");
			}
			else
			{
				commandExecutor?.Execute(command);
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
				commandContext?.Dispose();
			}

			OnDisposed?.Invoke();
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~DiagnosticCommandContext()
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

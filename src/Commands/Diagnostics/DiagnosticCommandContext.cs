﻿using Microsoft.Extensions.Logging;

namespace Commands.Diagnostics
{
	public class DiagnosticCommandContext(ICommandContext commandContext) : ICommandContext
	{
		private bool disposedValue;
		public Action? OnDisposed { get; set; }
		public Action? OnExecuteCancelled { get; set; }
		public Action<ICommand>? OnExecuted { get; set; }
		public Action<ICommand>? OnExecuting { get; set; }
		public Action<ICommand>? OnExecuteChecking { get; set; }
		public Action<ICommand>? OnExecuteChecked { get; set; }

		public ILogger Logger => commandContext.Logger;
		public bool IsDisposed { get; }
		public ICommandResolver CommandResolver => commandContext.CommandResolver;

		public virtual TCommand? Execute<TCommand>(TCommand? command)
			where TCommand : class, ICommand
		{
			ObjectDisposedException.ThrowIf(IsDisposed, nameof(CommandContext));

			if (command is null)
			{
				Logger?.LogWarning(CommandEventIds.CommandNotProvided, "No command provided for execution.");
				return command;
			}

			string typeName = FriendlyName.GetFriendlyName(command.GetType());
			string fullName = !string.IsNullOrEmpty(command.Name) && !string.Equals(typeName, command.Name, StringComparison.OrdinalIgnoreCase)
				? $"{FriendlyName.GetFriendlyName(command.GetType())} - {command.Name}"
				: typeName;

			OnExecuteChecked?.Invoke(command);
			if (!command.CanExecute(this))
			{
				// log that the command cannot be executed
				Logger?.LogWarning(CommandEventIds.CommandCannotExecute, "Command {CommandName} cannot be executed: {ExceptionMessage}", fullName, command.ExceptionMessage);
				OnExecuteCancelled?.Invoke();
				return command;
			}
			OnExecuteChecked?.Invoke(command);

			try
			{
				Logger?.LogDebug(CommandEventIds.CommandExecutionStarted, "Executing command: {CommandName}", fullName);
				OnExecuting?.Invoke(command);
				command.Execute(this);
				OnExecuted?.Invoke(command);

				if (string.IsNullOrEmpty(command.ExceptionMessage))
				{
					Logger?.LogDebug(CommandEventIds.CommandExecutionCompleted, "Command executed successfully: {CommandName}", fullName);
				}
				else
				{
					Logger?.LogInformation(CommandEventIds.CommandCompletedWithMessage, "Command executed with message: {Message} for command: {CommandName}", command.ExceptionMessage, fullName);
				}
			}
			catch (Exception ex)
			{
				Logger?.LogError(CommandEventIds.CommandExecutionError, ex, "Error executing command: {CommandName}", fullName);
				throw;
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

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Commands.Diagnostics
{
	/// <summary>
	/// Executes <see cref="ICommand"/> instances and provides diagnostic hooks and logging around
	/// the execution lifecycle.
	/// </summary>
	/// <param name="Logger">The logger used to emit diagnostic events. May be <c>null</c>.</param>
	public class DiagnosticCommandExecutor(ILogger Logger) : ICommandExecutor
	{
		/// <summary>
		/// Invoked before the command's <see cref="ICommand.CanExecute"/> check is performed.
		/// </summary>
		public Action<ICommand>? OnExecuteChecking { get; set; }

		/// <summary>
		/// Invoked after the command's <see cref="ICommand.CanExecute"/> check has completed.
		/// </summary>
		public Action<ICommand>? OnExecuteChecked { get; set; }

		/// <summary>
		/// Invoked when a command's execution is cancelled because it cannot execute.
		/// </summary>
		public Action<ICommand>? OnExecutionCancelled { get; set; }

		/// <summary>
		/// Invoked immediately before <see cref="ICommand.Execute"/> is called.
		/// </summary>
		public Action<ICommand>? OnBeforeExecuting { get; set; }

		/// <summary>
		/// Invoked immediately after <see cref="ICommand.Execute"/> returns successfully.
		/// </summary>
		public Action<ICommand>? OnAfterExecuting { get; set; }

		/// <summary>
		/// Invoked when an unhandled <see cref="Exception"/> is thrown while executing a command.
		/// The exception is also logged before this callback is invoked.
		/// </summary>
		public Action<ICommand, Exception>? OnExecutionError { get; set; }

		/// <summary>
		/// Invoked when command execution has ended. The final <see cref="CommandStatus"/> is provided.
		/// </summary>
		public Action<ICommand, CommandStatus>? OnExecutionEnded { get; set; }

		/// <summary>
		/// Executes the supplied <paramref name="command"/> using the provided <paramref name="commandContext"/>.
		/// Logs diagnostic events and invokes the lifecycle callbacks defined on this executor.
		/// </summary>
		/// <param name="commandContext">The context to provide to the command during execution. Must not be <c>null</c>.</param>
		/// <param name="command">The command instance to execute. Must not be <c>null</c>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="command"/> is <c>null</c>.</exception>
		/// <exception cref="Exception">Any exception thrown by <see cref="ICommand.Execute"/> is propagated to the caller after being logged and forwarded to <see cref="OnExecutionError"/>.</exception>
		public void Execute([Required] ICommandContext commandContext, [Required] ICommand command)
		{
			ArgumentNullException.ThrowIfNull(command, nameof(command));

			string typeName = FriendlyName.GetFriendlyName(command.GetType());
			string fullName = !string.IsNullOrEmpty(command.Name) && !string.Equals(typeName, command.Name, StringComparison.OrdinalIgnoreCase)
				? $"{FriendlyName.GetFriendlyName(command.GetType())} - {command.Name}"
				: typeName;

			OnExecuteChecking?.Invoke(command);
			bool canExecute = command.CanExecute(commandContext);
			OnExecuteChecked?.Invoke(command);

			if (canExecute)
			{
				// log that the command cannot be executed
				Logger?.LogWarning(CommandEventIds.CommandCannotExecute, "Command {CommandName} cannot be executed: {ExceptionMessage}", fullName, command.ExceptionMessage);
				OnExecutionCancelled?.Invoke(command);
				return;
			}

			try
			{
				Logger?.LogDebug(CommandEventIds.CommandExecutionStarted, "Executing command: {CommandName}", fullName);
				OnBeforeExecuting?.Invoke(command);
				command.Execute(commandContext);
				OnAfterExecuting?.Invoke(command);
				OnExecutionEnded?.Invoke(command, command.Status);

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
				OnExecutionError?.Invoke(command, ex);
				throw;
			}
		}
	}
}

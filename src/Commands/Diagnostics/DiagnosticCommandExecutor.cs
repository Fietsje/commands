using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Commands.Diagnostics
{
	public class DiagnosticCommandExecutor(ICommandContext CommandContext, ILogger Logger) : ICommandExecutor
	{
		public Action<ICommand>? OnExecuteChecking { get; set; }
		public Action<ICommand>? OnExecuteChecked { get; set; }
		public Action<ICommand>? OnExecutionCancelled { get; set; }
		public Action<ICommand>? OnBeforeExecuting { get; set; }
		public Action<ICommand>? OnAfterExecuting { get; set; }
		public Action<ICommand, Exception>? OnExecutionError { get; set; }
		public Action<ICommand, CommandStatus>? OnExecutionEnded { get; set; }

		public void Execute([Required] ICommand command)
		{
			ArgumentNullException.ThrowIfNull(command, nameof(command));

			string typeName = FriendlyName.GetFriendlyName(command.GetType());
			string fullName = !string.IsNullOrEmpty(command.Name) && !string.Equals(typeName, command.Name, StringComparison.OrdinalIgnoreCase)
				? $"{FriendlyName.GetFriendlyName(command.GetType())} - {command.Name}"
				: typeName;

			OnExecuteChecking?.Invoke(command);
			bool canExecute = command.CanExecute(CommandContext);
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
				command.Execute(CommandContext);
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

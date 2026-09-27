using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Commands
{
	/// <summary>
	/// Executes an <see cref="ICommand"/> instance and logs diagnostic events during execution.
	/// </summary>
	/// <param name="Logger">The <see cref="ILogger"/> used to emit diagnostic events. May be <c>null</c>.</param>
	public class CommandExecutor : ICommandExecutor
	{
		private readonly ILogger _logger;

		public CommandExecutor(ILogger logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Executes the supplied <paramref name="command"/> using the provided <paramref name="commandContext"/>.
		/// Logs execution start, completion, information messages, and errors to the configured <see cref="ILogger"/>.
		/// </summary>
		/// <param name="commandContext">The context supplied to the command during execution. Must not be <c>null</c>.</param>
		/// <param name="command">The command instance to execute. Must implement <see cref="ICommand"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="command"/> is <c>null</c>.</exception>
		/// <exception cref="Exception">Any exception thrown by <see cref="ICommand.Execute"/> is propagated to the caller after logging.</exception>
		public void Execute([Required] ICommandContext commandContext, [Required] ICommand command)
		{
			string typeName = FriendlyName.GetFriendlyName(command.GetType());
			string fullName = !string.IsNullOrEmpty(command.Name) && !string.Equals(typeName, command.Name, StringComparison.OrdinalIgnoreCase)
				? $"{FriendlyName.GetFriendlyName(command.GetType())} - {command.Name}"
				: typeName;

			if (!command.CanExecute(commandContext))
			{
				// log that the command cannot be executed
				_logger?.LogWarning(CommandEventIds.CommandCannotExecute, "Command {CommandName} cannot be executed: {ExceptionMessage}", fullName, command.ExceptionMessage);
				return;
			}

			try
			{
				_logger?.LogDebug(CommandEventIds.CommandExecutionStarted, "Executing command: {CommandName}", fullName);
				command.Execute(commandContext);

				if (string.IsNullOrEmpty(command.ExceptionMessage))
				{
					_logger?.LogDebug(CommandEventIds.CommandExecutionCompleted, "Command executed successfully: {CommandName}", fullName);
				}
				else
				{
					_logger?.LogInformation(CommandEventIds.CommandCompletedWithMessage, "Command executed with message: {Message} for command: {CommandName}", command.ExceptionMessage, fullName);
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(CommandEventIds.CommandExecutionError, ex, "Error executing command: {CommandName}", fullName);
				throw;
			}
		}
	}
}

using Microsoft.Extensions.Logging;

namespace Commands
{
	/// <summary>
	/// Executes a configured <typeparamref name="TCommand"/> instance created from an <see cref="ICommandContext"/>.
	/// </summary>
	/// <typeparam name="TCommand">The concrete command type. Must be a reference type that implements <see cref="ICommand"/> and provide a public parameterless constructor.</typeparam>
	/// <param name="context">The command context used to create and execute the command.</param>
	/// <param name="configuration">The configuration action to apply before execution.</param>
	/// <param name="condition">When <c>true</c>, the command will be configured and executed.</param>
	internal class CommandExecutor<TCommand>(ICommandContext context, Action<TCommand> configuration, bool condition) : ICommandExecutor<TCommand>
		where TCommand : class, ICommand, new()
	{
		/// <summary>
		/// Creates the command, applies the configured <paramref name="configuration"/> and executes it if <c>condition</c> is <c>true</c>.
		/// </summary>
		/// <returns>The created (and possibly executed) command instance, or <c>null</c> if execution produced no result.</returns>
		public TCommand? Execute()
		{
			TCommand command = context.Create<TCommand>();
			if (condition)
			{
				configuration(command);
				context.Execute(command);
			}
			return command;
		}
	}

	public class CommandExecutor(ICommandContext CommandContext, ILogger Logger) : ICommandExecutor
	{

		public void Execute(ICommand command)
		{
			string typeName = FriendlyName.GetFriendlyName(command.GetType());
			string fullName = !string.IsNullOrEmpty(command.Name) && !string.Equals(typeName, command.Name, StringComparison.OrdinalIgnoreCase)
				? $"{FriendlyName.GetFriendlyName(command.GetType())} - {command.Name}"
				: typeName;

			if (!command.CanExecute(CommandContext))
			{
				// log that the command cannot be executed
				Logger?.LogWarning(CommandEventIds.CommandCannotExecute, "Command {CommandName} cannot be executed: {ExceptionMessage}", fullName, command.ExceptionMessage);
				return;
			}

			try
			{
				Logger?.LogDebug(CommandEventIds.CommandExecutionStarted, "Executing command: {CommandName}", fullName);
				command.Execute(CommandContext);

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
		}
	}
}

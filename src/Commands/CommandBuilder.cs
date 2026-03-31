namespace Commands
{
	/// <summary>
	/// Builds and optionally executes a <typeparamref name="TCommand"/> instance using an <see cref="ICommandContext"/>.
	/// </summary>
	/// <typeparam name="TCommand">The concrete command type. Must be a reference type that implements <see cref="ICommand"/> and provide a public parameterless constructor.</typeparam>
	/// <param name="context">The command context used to create and execute commands.</param>
	/// <param name="condition">When <c>true</c>, the command will be executed after creation.</param>
	internal class CommandBuilder<TCommand>(ICommandContext context, bool condition) : ICommandBuilder<TCommand>
		where TCommand : class, ICommand, new()
	{
		/// <summary>
		/// Applies the given <paramref name="configuration"/> to the command and returns an executor.
		/// </summary>
		/// <param name="configuration">An action that configures the command instance prior to execution. This parameter must not be <c>null</c>.</param>
		/// <returns>An <see cref="ICommandExecutor{TCommand}"/> which will execute the configured command.</returns>
		public ICommandExecutor<TCommand> Configure(Action<TCommand> configuration)
		{
			return new CommandExecutor<TCommand>(context, configuration, condition);
		}

		/// <summary>
		/// Creates the command and, if <c>condition</c> is <c>true</c>, executes it using the provided <see cref="ICommandContext"/>.
		/// </summary>
		/// <returns>The created (and possibly executed) command instance, or <c>null</c> if execution produced no result.</returns>
		public TCommand? Execute()
		{
			TCommand command = context.Create<TCommand>();
			if (condition)
			{
				context.Execute(command);
			}

			return command;
		}
	}
}

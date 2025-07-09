namespace Commands
{
	internal class CommandBuilder<TCommand>(ICommandContext context, bool condition) : ICommandBuilder<TCommand>
		where TCommand : class, ICommand, new()
	{
		public ICommandExecutor<TCommand> Configure(Action<TCommand> configuration)
		{
			return new CommandExecutor<TCommand>(context, configuration, condition);
		}

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

	internal class CommandExecutor<TCommand>(ICommandContext context, Action<TCommand> configuration, bool condition) : ICommandExecutor<TCommand>
		where TCommand : class, ICommand, new()
	{
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
}

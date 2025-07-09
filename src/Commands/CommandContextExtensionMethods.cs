namespace Commands
{
	public static class CommandContextExtensionMethods
	{
		public static ICommandBuilder<TCommand> Build<TCommand>(this ICommandContext context)
			where TCommand : class, ICommand, new()
		{
			return new CommandBuilder<TCommand>(context, true);
		}

		public static TCommand Create<TCommand>(this ICommandContext context)
			where TCommand : class, ICommand, new()
		{
			return context.CommandResolver.Resolve<TCommand>();
		}

		public static TCommand? Execute<TCommand>(this ICommandContext context)
			where TCommand : class, ICommand, new()
		{
			return context.Execute(context.Create<TCommand>());
		}

		public static ICommandBuilder<TCommand> When<TCommand>(this ICommandContext context, bool condition)
			where TCommand : class, ICommand, new()
		{
			return new CommandBuilder<TCommand>(context, condition);
		}
	}
}

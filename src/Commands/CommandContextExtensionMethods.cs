using System;

namespace Commands
{
	public static class CommandContextExtensionMethods
	{
		public static TCommand Execute<TCommand>(this ICommandContext context)
			where TCommand : class, ICommand, new()
		{
			return Execute<TCommand>(context, command => { });
		}

		public static TCommand Execute<TCommand>(this ICommandContext context, Action<TCommand> action)
			where TCommand : class, ICommand, new()
		{
			TCommand command = context.CommandResolver.Resolve<TCommand>();
			action(command);
			return context.Execute(command);
		}

		public static ICommandAnalysis<TCommand> Analyze<TCommand>(this ICommandContext commandContext)
			where TCommand : class, ICommand, new()
		{
			return new CommandAnalysis<TCommand>(commandContext);
		}
	}
}

namespace Commands
{
	public interface ICommandBuilder<TCommand>
		where TCommand : class, ICommand, new()
	{
		ICommandExecutor<TCommand> Configure(Action<TCommand> configuration);
		TCommand? Execute();
	}

}

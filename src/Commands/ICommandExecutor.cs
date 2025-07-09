namespace Commands
{
	public interface ICommandExecutor<TCommand>
		where TCommand : class, ICommand, new()
	{
		TCommand? Execute();
	}
}

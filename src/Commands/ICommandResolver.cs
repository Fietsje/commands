namespace Commands
{
	public interface ICommandResolver
	{
		void Clear();

		TCommand Resolve<TCommand>()
			where TCommand : class, ICommand, new();

		IDisposable Register<TCommand>(Func<TCommand> factory)
			where TCommand : class, ICommand, new();

		IDisposable Register(Type commandType, Func<ICommand> factory);
	}
}

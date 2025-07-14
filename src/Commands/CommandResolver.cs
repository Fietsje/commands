namespace Commands
{
	public class CommandResolver : ICommandResolver
	{
		private readonly Dictionary<Type, Func<ICommand>> _factories = new();

		public void Clear()
		{
			_factories.Clear();
		}

		public IDisposable Register(Type commandType, Func<ICommand> factory)
		{
			if (_factories.ContainsKey(commandType))
			{
				throw new InvalidOperationException($"Command type {commandType.Name} is already registered.");
			}
			return new CommandUnresolver(() => { _factories.Remove(commandType); });
		}

		public IDisposable Register<TCommand>(Func<TCommand> factory)
			where TCommand : class, ICommand, new()
		{
			return Register(typeof(TCommand), factory);
		}

		public virtual TCommand Resolve<TCommand>()
			where TCommand : class, ICommand, new()
		{
			_factories.TryGetValue(typeof(TCommand), out var factory);
			if (factory is not null)
			{
				return (TCommand)factory();
			}
			return new TCommand()!;
		}
	}
}

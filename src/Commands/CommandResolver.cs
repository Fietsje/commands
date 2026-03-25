namespace Commands
{
	/// <summary>
	/// Default implementation of <see cref="ICommandResolver"/> that manages factories used to create <see cref="ICommand"/> instances.
	/// </summary>
	/// <remarks>
	/// Registrations return an <see cref="IDisposable"/> which, when disposed, should remove the registration for the associated command type.
	/// </remarks>
	public class CommandResolver : ICommandResolver
	{
		private readonly Dictionary<Type, Func<ICommand>> _factories = new();

		/// <summary>
		/// Removes all registered command factories.
		/// </summary>
		public void Clear()
		{
			_factories.Clear();
		}

		/// <summary>
		/// Registers a factory for the specified command <paramref name="commandType"/>.
		/// </summary>
		/// <param name="commandType">The runtime type of the command to register. The type is expected to implement <see cref="ICommand"/>.</param>
		/// <param name="factory">A factory delegate that creates instances of the specified command type.</param>
		/// <returns>
		/// An <see cref="IDisposable"/> that removes the registration when disposed.
		/// </returns>
		/// <exception cref="InvalidOperationException">Thrown when a factory for the specified <paramref name="commandType"/> is already registered.</exception>
		public IDisposable Register(Type commandType, Func<ICommand> factory)
		{
			if (_factories.ContainsKey(commandType))
			{
				throw new InvalidOperationException($"Command type {commandType.Name} is already registered.");
			}

			_factories[commandType] = factory;
			return new CommandUnresolver(() => { _factories.Remove(commandType); });
		}

		/// <summary>
		/// Registers a factory that creates instances of <typeparamref name="TCommand"/>.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type the factory produces. Must implement <see cref="ICommand"/> and provide a parameterless constructor.</typeparam>
		/// <param name="factory">A factory delegate that returns new instances of <typeparamref name="TCommand"/>.</param>
		/// <returns>
		/// An <see cref="IDisposable"/> which, when disposed, removes the registration for <typeparamref name="TCommand"/>.
		/// </returns>
		public IDisposable Register<TCommand>(Func<TCommand> factory)
			where TCommand : class, ICommand, new()
		{
			return Register(typeof(TCommand), factory);
		}

		/// <summary>
		/// Resolves a new instance of the requested command type.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type to resolve. Must implement <see cref="ICommand"/> and provide a parameterless constructor.</typeparam>
		/// <returns>
		/// A new instance of <typeparamref name="TCommand"/> created by the registered factory, or a fresh instance via <c>new TCommand()</c> if no factory is registered.
		/// </returns>
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

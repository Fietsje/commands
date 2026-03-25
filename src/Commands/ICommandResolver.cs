namespace Commands
{
	/// <summary>
	/// Resolves, registers and manages command factory registrations used to create <see cref="ICommand"/> instances.
	/// </summary>
	/// <remarks>
	/// Implementations provide a way to register factories for specific command types and to resolve new command instances
	/// using those factories. Registrations return an <see cref="IDisposable"/> which should remove the registration when disposed.
	/// </remarks>
	public interface ICommandResolver
	{
		/// <summary>
		/// Clears all registered command factories.
		/// </summary>
		void Clear();

		/// <summary>
		/// Resolves a new instance of the requested command type.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type to resolve. Must implement <see cref="ICommand"/>, be a reference type and have a public parameterless constructor.</typeparam>
		/// <returns>A new instance of <typeparamref name="TCommand"/> created by the registered factory or default factory.</returns>
		TCommand Resolve<TCommand>()
			where TCommand : class, ICommand, new();

		/// <summary>
		/// Registers a factory that creates instances of <typeparamref name="TCommand"/>.
		/// </summary>
		/// <typeparam name="TCommand">The command type the factory produces. Must implement <see cref="ICommand"/>, be a reference type and have a public parameterless constructor.</typeparam>
		/// <param name="factory">A factory delegate that returns new instances of <typeparamref name="TCommand"/>.</param>
		/// <returns>
		/// An <see cref="IDisposable"/> which, when disposed, removes the registration for the provided command type.
		/// </returns>
		IDisposable Register<TCommand>(Func<TCommand> factory)
			where TCommand : class, ICommand, new();

		/// <summary>
		/// Registers a factory that creates instances for the specified command <paramref name="commandType"/>.
		/// </summary>
		/// <param name="commandType">The runtime <see cref="Type"/> of command the factory produces. Must implement <see cref="ICommand"/>.</param>
		/// <param name="factory">A factory delegate that returns new instances of <see cref="ICommand"/>.</param>
		/// <returns>
		/// An <see cref="IDisposable"/> which, when disposed, removes the registration for the specified <paramref name="commandType"/>.
		/// </returns>
		IDisposable Register(Type commandType, Func<ICommand> factory);
	}
}

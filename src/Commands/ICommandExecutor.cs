namespace Commands
{
	/// <summary>
	/// Executes a configured <typeparamref name="TCommand"/> instance.
	/// </summary>
	/// <typeparam name="TCommand">
	/// The concrete command type to execute. Must be a reference type that implements <see cref="ICommand"/>
	/// and provide a public parameterless constructor.
	/// </typeparam>
	public interface ICommandExecutor<TCommand>
		where TCommand : class, ICommand, new()
	{
		/// <summary>
		/// Executes the command and returns the command instance after execution.
		/// </summary>
		/// <returns>
		/// The executed <typeparamref name="TCommand"/> instance, or <c>null</c> if execution produced no result.
		/// </returns>
		TCommand? Execute();
	}

	/// <summary>
	/// Executes a configured <typeparamref name="ICommand"/> instance.
	/// </summary>
	public interface ICommandExecutor
	{
		/// <summary>
		/// Executes the command.
		/// </summary>
		void Execute(ICommand command);
	}

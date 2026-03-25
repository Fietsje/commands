namespace Commands
{
	/// <summary>
	/// Provides analysis information for a command of type <typeparamref name="TCommand"/>.
	/// </summary>
	/// <typeparam name="TCommand">
	/// The concrete command type being analyzed. Must be a reference type that implements <see cref="ICommand"/>
	/// and provide a public parameterless constructor.
	/// </typeparam>
	/// <remarks>
	/// Implementations expose the analyzed <see cref="ICommand"/>, whether it can be executed, and an optional
	/// exception message explaining why execution is not possible. Deconstruction helpers are provided to enable
	/// concise pattern matching and tuple-like access.
	/// </remarks>
	public interface ICommandAnalysis<TCommand>
		where TCommand : class, ICommand, new()
	{
		/// <summary>
		/// Gets the analyzed command instance.
		/// </summary>
		ICommand Command { get; }

		/// <summary>
		/// Gets a value indicating whether the analyzed command can be executed.
		/// </summary>
		bool CanExecute { get; }

		/// <summary>
		/// Gets an optional exception message describing why the command cannot be executed, if any.
		/// </summary>
		string? ExceptionMessage { get; }

		/// <summary>
		/// Deconstructs the analysis into its primary values.
		/// </summary>
		/// <param name="canExecute">Receives the value of <see cref="CanExecute"/>.</param>
		/// <param name="exceptionMessage">Receives the value of <see cref="ExceptionMessage"/>.</param>
		void Deconstruct(out bool canExecute, out string? exceptionMessage);

		/// <summary>
		/// Deconstructs the analysis into its primary values including the analyzed command.
		/// </summary>
		/// <param name="canExecute">Receives the value of <see cref="CanExecute"/>.</param>
		/// <param name="exceptionMessage">Receives the value of <see cref="ExceptionMessage"/>.</param>
		/// <param name="command">Receives the analyzed <see cref="ICommand"/> instance.</param>
		void Deconstruct(out bool canExecute, out string? exceptionMessage, out ICommand command);
	}
}

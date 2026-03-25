namespace Commands
{
	/// <summary>
	/// Represents a command that can be executed within an <see cref="ICommandContext"/>.
	/// </summary>
	/// <remarks>
	/// Implementations contain execution state and optionally produce a result via <see cref="ICommand{TResult}.Result"/>.
	/// </remarks>
	public interface ICommand
	{
		/// <summary>
		/// Gets the optional name of the command.
		/// </summary>
		string? Name { get; }

		/// <summary>
		/// Gets a human-readable message describing an exception that occurred during execution, if any.
		/// </summary>
		string? ExceptionMessage { get; }

		/// <summary>
		/// Gets an implementation-defined exception code or number associated with an error that occurred during execution.
		/// </summary>
		int ExceptionNumber { get; }

		/// <summary>
		/// Gets the current <see cref="CommandStatus"/> for the command.
		/// </summary>
		/// <seealso cref="CommandStatus"/>
		CommandStatus Status { get; }

		/// <summary>
		/// Determines whether the command can be executed in the provided <see cref="ICommandContext"/>.
		/// </summary>
		/// <param name="commandContext">The context in which the command would execute. This parameter must not be <c>null</c>.</param>
		/// <returns><c>true</c> if the command can be executed in the given context; otherwise, <c>false</c>.</returns>
		bool CanExecute(ICommandContext commandContext);

		/// <summary>
		/// Executes the command using the provided <see cref="ICommandContext"/>.
		/// </summary>
		/// <param name="commandContext">The context to use during execution. This parameter must not be <c>null</c>.</param>
		void Execute(ICommandContext commandContext);
	}

	/// <summary>
	/// Represents a command that produces a result of type <typeparamref name="TResult"/>.
	/// </summary>
	/// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
	public interface ICommand<out TResult> : ICommand
	{
		/// <summary>
		/// Gets the result produced by the command after successful execution.
		/// </summary>
		TResult Result { get; }
	}
}
namespace Commands
{

	/// <summary>
	/// Provides analysis information for a specific command type <typeparamref name="TCommand"/>.
	/// </summary>
	/// <typeparam name="TCommand">
	/// The concrete command type being analyzed. Must be a reference type that implements <see cref="ICommand"/>
	/// and provide a public parameterless constructor.
	/// </typeparam>
	internal class CommandAnalysis<TCommand> : ICommandAnalysis<TCommand>
		where TCommand : class, ICommand, new()
	{
		private readonly ICommand _command;
		private readonly ICommandContext _commandContext;

		/// <summary>
		/// Gets the analyzed <see cref="ICommand"/> instance.
		/// </summary>
		public ICommand Command => _command;

		/// <summary>
		/// Gets a value indicating whether the analyzed command can be executed in the provided context.
		/// </summary>
		public bool CanExecute { get; private set; }

		/// <summary>
		/// Gets an optional exception message produced by the analyzed command, if any.
		/// </summary>
		public string? ExceptionMessage => _command?.ExceptionMessage;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandAnalysis{TCommand}"/> class
		/// and evaluates whether the created command can execute in the given <paramref name="commandContext"/>.
		/// </summary>
		/// <param name="commandContext">The context used to create and evaluate the command. Must not be <c>null</c>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="commandContext"/> is <c>null</c>.</exception>
		public CommandAnalysis(ICommandContext commandContext)
		{
			_commandContext = commandContext ?? throw new ArgumentNullException(nameof(commandContext));
			_command = _commandContext.Create<TCommand>();
			CanExecute = _command.CanExecute(_commandContext);
		}

		/// <summary>
		/// Deconstructs the analysis into its primary values: <see cref="CanExecute"/> and <see cref="ExceptionMessage"/>.
		/// </summary>
		/// <param name="canExecute">Receives the value of <see cref="CanExecute"/>.</param>
		/// <param name="exceptionMessage">Receives the value of <see cref="ExceptionMessage"/>.</param>
		public void Deconstruct(out bool canExecute, out string? exceptionMessage)
		{
			Deconstruct(out canExecute, out exceptionMessage, out _);
		}

		/// <summary>
		/// Deconstructs the analysis into its primary values including the analyzed <see cref="ICommand"/>.
		/// </summary>
		/// <param name="canExecute">Receives the value of <see cref="CanExecute"/>.</param>
		/// <param name="exceptionMessage">Receives the value of <see cref="ExceptionMessage"/>.</param>
		/// <param name="command">Receives the analyzed <see cref="ICommand"/> instance.</param>
		public void Deconstruct(out bool canExecute, out string? exceptionMessage, out ICommand command)
		{
			canExecute = CanExecute;
			exceptionMessage = ExceptionMessage;
			command = Command;
		}
	}
}

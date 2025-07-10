namespace Commands
{
	internal class CommandAnalyzer<TCommand> : ICommandAnalyzer<TCommand>
		where TCommand : class, ICommand, new()
	{
		private readonly ICommand _command;
		private readonly ICommandContext _commandContext;

		public ICommand Command => _command;
		public bool CanExecute { get; private set; }
		public string? ExceptionMessage => _command?.ExceptionMessage;

		public CommandAnalyzer(ICommandContext commandContext)
		{
			_commandContext = commandContext ?? throw new ArgumentNullException(nameof(commandContext));
			_command = _commandContext.Create<TCommand>();
			CanExecute = _command.CanExecute(_commandContext);
		}

		public void Deconstruct(out bool canExecute, out string? exceptionMessage)
		{
			Deconstruct(out canExecute, out exceptionMessage, out _);
		}

		public void Deconstruct(out bool canExecute, out string? exceptionMessage, out ICommand command)
		{
			canExecute = CanExecute;
			exceptionMessage = ExceptionMessage;
			command = Command;
		}
	}
}

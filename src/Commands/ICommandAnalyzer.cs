namespace Commands
{
	public interface ICommandAnalyzer<TCommand>
		where TCommand : class, ICommand, new()
	{
		ICommand Command { get; }
		bool CanExecute { get; }
		string? ExceptionMessage { get; }

		void Deconstruct(out bool canExecute, out string? exceptionMessage);
		void Deconstruct(out bool canExecute, out string? exceptionMessage, out ICommand command);
	}
}

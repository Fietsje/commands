namespace Commands
{
	public interface ICommand
	{
		public string? Name { get; }
		string? ExceptionMessage { get; }
		int ExceptionNumber { get; }
		CommandStatus Status { get; }

		bool CanExecute(ICommandContext commandContext);

		void Execute(ICommandContext commandContext);
	}
}

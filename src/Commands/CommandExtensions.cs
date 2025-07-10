namespace Commands
{
	public static class CommandExtensions
	{
		public static void Deconstruct(this ICommand command, out CommandStatus status, out int exceptionNumber, out string? message)
		{
			status = command.Status;
			message = command.ExceptionMessage;
			exceptionNumber = command.ExceptionNumber;
		}

		public static void Deconstruct<TResult>(this ICommand<TResult> command, out CommandStatus status, out int exceptionNumber, out string? message, out TResult result)
		{
			status = command.Status;
			message = command.ExceptionMessage;
			exceptionNumber = command.ExceptionNumber;
			result = command.Result;
		}
	}
}

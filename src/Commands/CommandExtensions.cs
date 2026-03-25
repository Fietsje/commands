namespace Commands
{
	/// <summary>
	/// Provides extension methods for <see cref="ICommand"/> instances, including deconstruction helpers
	/// that enable tuple-like extraction of command status, error information and result values.
	/// </summary>
	public static class CommandExtensions
	{
		/// <summary>
		/// Deconstructs an <see cref="ICommand"/> into its runtime status, exception number and exception message.
		/// </summary>
		/// <param name="command">The command to deconstruct. This parameter must not be <c>null</c>.</param>
		/// <param name="status">Receives the command's <see cref="CommandStatus"/>.</param>
		/// <param name="exceptionNumber">Receives the command's exception number.</param>
		/// <param name="message">Receives the command's exception message, or <c>null</c> if none.</param>
		public static void Deconstruct(this ICommand command, out CommandStatus status, out int exceptionNumber, out string? message)
		{
			status = command.Status;
			message = command.ExceptionMessage;
			exceptionNumber = command.ExceptionNumber;
		}

		/// <summary>
		/// Deconstructs an <see cref="ICommand{TResult}"/> into its runtime status, exception number, exception message and result.
		/// </summary>
		/// <typeparam name="TResult">The type of the command result.</typeparam>
		/// <param name="command">The command to deconstruct. This parameter must not be <c>null</c>.</param>
		/// <param name="status">Receives the command's <see cref="CommandStatus"/>.</param>
		/// <param name="exceptionNumber">Receives the command's exception number.</param>
		/// <param name="message">Receives the command's exception message, or <c>null</c> if none.</param>
		/// <param name="result">Receives the command's result value.</param>
		public static void Deconstruct<TResult>(this ICommand<TResult> command, out CommandStatus status, out int exceptionNumber, out string? message, out TResult result)
		{
			status = command.Status;
			message = command.ExceptionMessage;
			exceptionNumber = command.ExceptionNumber;
			result = command.Result;
		}
	}
}

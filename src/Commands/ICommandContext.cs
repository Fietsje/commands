using Microsoft.Extensions.Logging;

namespace Commands
{

	/// <summary>
	/// Represents a contextual environment for executing commands.
	/// Provides access to logging, a command resolver and manages the lifetime of command execution resources.
	/// </summary>
	/// <remarks>
	/// Implementations are disposable and should release any resources when <see cref="IDisposable.Dispose"/> is called.
	/// </remarks>
	public interface ICommandContext : IDisposable
	{
		/// <summary>
		/// Gets the logger used for diagnostics and tracing during command execution.
		/// </summary>
		ILogger Logger { get; }

		/// <summary>
		/// Gets a value indicating whether this context has been disposed.
		/// </summary>
		bool IsDisposed { get; }

		/// <summary>
		/// Gets the resolver that can locate command handlers or other command-related services.
		/// </summary>
		ICommandResolver CommandResolver { get; }

		/// <summary>
		/// Executes the specified command within this context.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type. Must implement <see cref="ICommand"/> and be a reference type.</typeparam>
		/// <param name="command">The command instance to execute. May be <c>null</c>.</param>
		/// <returns>The executed command instance or <c>null</c> if execution produced no result.</returns>
		TCommand? Execute<TCommand>(TCommand? command) where TCommand : class, ICommand;
	}
}
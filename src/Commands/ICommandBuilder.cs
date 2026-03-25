using System.ComponentModel.DataAnnotations;

namespace Commands
{
	/// <summary>
	/// Builds and configures a <typeparamref name="TCommand"/> instance and produces an executor to run it.
	/// </summary>
	/// <typeparam name="TCommand">
	/// The concrete command type to build. Must be a reference type that implements <see cref="ICommand"/>
	/// and provide a public parameterless constructor.
	/// </typeparam>
	public interface ICommandBuilder<TCommand>
		where TCommand : class, ICommand, new()
	{
		/// <summary>
		/// Applies the provided configuration action to the command being built and returns an executor for execution.
		/// </summary>
		/// <param name="configuration">An action that configures the command instance. This parameter must not be <c>null</c>.</param>
		/// <returns>An <see cref="ICommandExecutor{TCommand}"/> capable of executing the configured command.</returns>
		ICommandExecutor<TCommand> Configure([Required] Action<TCommand> configuration);

		/// <summary>
		/// Builds and executes the command.
		/// </summary>
		/// <returns>
		/// The executed command instance, or <c>null</c> if execution produced no result.
		/// </returns>
		TCommand? Execute();
	}

}

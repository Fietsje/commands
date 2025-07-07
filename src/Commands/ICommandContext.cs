using Microsoft.Extensions.Logging;

namespace Commands
{
	public interface ICommandContext : IDisposable
	{
		ILogger Logger { get; }
		bool IsDisposed { get; }
		ICommandResolver CommandResolver { get; }

		TCommand? Execute<TCommand>(TCommand? command) where TCommand : class, ICommand;
	}
}
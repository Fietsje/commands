using Commands.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Commands
{
	public class CommandContext : ICommandContext
	{
		private readonly ICommandExecutor commandExecutor;
		protected IServiceProvider ServiceProvider { get; private set; }

		public ILogger Logger { get; private set; }
		public bool IsDisposed { get; private set; }
		public ICommandResolver CommandResolver { get; private set; }

		public CommandContext(IServiceProvider serviceProvider)
		{
			ArgumentNullException.ThrowIfNull(serviceProvider);

			LoggerCollection loggers = new(serviceProvider.GetServices<ILogger>());
			CommandResolver = serviceProvider.GetService(typeof(ICommandResolver)) as ICommandResolver;
			commandExecutor = serviceProvider.GetService(typeof(ICommandExecutor)) as ICommandExecutor;
			ServiceProvider = serviceProvider;

			if (loggers.Count == 0)
			{
				loggers.AddLogger(new ConsoleLogger(LogLevel.Trace));
			}

			Logger = loggers;
			CommandResolver ??= new CommandResolver();
			commandExecutor ??= new CommandExecutor(Logger);
		}

		public virtual TCommand Execute<TCommand>(TCommand command)
			where TCommand : class, ICommand
		{
			if (command is null)
			{
				Logger?.LogWarning(CommandEventIds.CommandNotProvided, "No command provided for execution.");
			}
			else
			{
				commandExecutor.Execute(this, command);
			}

			return command;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					Logger = null;
					ServiceProvider = null;
					CommandResolver = null;
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				IsDisposed = true;
			}
		}

		// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		~CommandContext()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

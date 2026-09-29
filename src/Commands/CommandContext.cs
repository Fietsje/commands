using Commands.Logging;
using Microsoft.Extensions.Logging;
using System;

namespace Commands
{
	public class CommandContext : ICommandContext
	{
		private readonly ICommandExecutor commandExecutor;
		private bool ownsLogger;
		private bool ownsResolver;
		private bool ownsExecutor;

		public ILogger Logger { get; private set; }
		public bool IsDisposed { get; private set; }
		public ICommandResolver CommandResolver { get; private set; }

		public CommandContext(ILogger logger = null, ICommandResolver resolver = null, ICommandExecutor executor = null)
		{
			Logger = logger;
			CommandResolver = resolver;
			commandExecutor = executor;

			if (Logger is null)
			{
				Logger = new ConsoleLogger(LogLevel.Trace);
				ownsLogger = true;
			}
			if (CommandResolver is null)
			{
				CommandResolver = new CommandResolver();
				ownsResolver = true;
			}
			if (commandExecutor is null)
			{
				commandExecutor = new CommandExecutor(Logger);
				ownsExecutor = true;
			}
		}

		public CommandContext(IServiceProvider serviceProvider)
		{
			ArgumentNullException.ThrowIfNull(serviceProvider);

			Logger = serviceProvider.GetService(typeof(ILogger)) as ILogger;
			CommandResolver = serviceProvider.GetService(typeof(ICommandResolver)) as ICommandResolver;
			commandExecutor = serviceProvider.GetService(typeof(ICommandExecutor)) as ICommandExecutor;

			if (Logger is null)
			{
				Logger = new ConsoleLogger(LogLevel.Trace);
				ownsLogger = true;
			}
			if (CommandResolver is null)
			{
				CommandResolver = new CommandResolver();
				ownsResolver = true;
			}
			if (commandExecutor is null)
			{
				commandExecutor = new CommandExecutor(Logger);
				ownsExecutor = true;
			}
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
					if (ownsLogger)
					{
						IDisposable disposableLogger = Logger as IDisposable;
						disposableLogger?.Dispose();
					}

					if (ownsResolver)
					{
						IDisposable disposableResolver = CommandResolver as IDisposable;
						disposableResolver?.Dispose();
					}

					if (ownsExecutor)
					{
						IDisposable disposableExecutor = commandExecutor as IDisposable;
						disposableExecutor?.Dispose();
					}
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

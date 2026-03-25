namespace Commands.Logging
{
	/// <summary>
	/// Represents a logical logging scope for <see cref="ConsoleLogger"/>.
	/// </summary>
	/// <remarks>
	/// Instances of this type are returned from <see cref="ILogger.BeginScope{TState}(TState)"/> and will invoke
	/// the provided cleanup action when disposed to remove the scope from the logger's internal stack.
	/// </remarks>
	internal sealed class ConsoleLoggerScope : IDisposable
	{
		private bool disposedValue;
		private readonly Action? _OnDisposing;

		/// <summary>
		/// Gets the state object associated with this scope.
		/// </summary>
		public object State { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleLoggerScope"/> class.
		/// </summary>
		/// <param name="state">The state object associated with the scope. This value is stored and later written by the logger when logging messages within the scope.</param>
		/// <param name="onDisposing">An action invoked when the scope is disposed. Typically used to remove the scope from the logger's internal collection.</param>
		public ConsoleLoggerScope(object state, Action onDisposing)
		{
			State = state;
			_OnDisposing = onDisposing;
		}

		/// <summary>
		/// Releases resources used by the instance.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> when called from <see cref="Dispose"/> to indicate managed resources should be released;
		/// <c>false</c> when called from a finalizer.
		/// </param>
		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
				_OnDisposing?.Invoke();
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~ConsoleLoggerScope()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// Invokes the cleanup action provided at construction time.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}

	/// <summary>
	/// Helper container used by <see cref="ConsoleLogger"/> to hold a reference to a <see cref="ConsoleLoggerScope"/>.
	/// </summary>
	internal sealed class ConsoleLoggerScopeItem
	{
		/// <summary>
		/// Gets or sets the associated <see cref="ConsoleLoggerScope"/>.
		/// </summary>
		public ConsoleLoggerScope? Scope { get; set; }
	}
}
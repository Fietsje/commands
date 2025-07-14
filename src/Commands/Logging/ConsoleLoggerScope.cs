namespace Commands.Logging
{
	internal class ConsoleLoggerScope : IDisposable
	{
		private bool disposedValue;
		private readonly Action? _OnDisposing;

		public object State { get; private set; }

		public ConsoleLoggerScope(object state, Action onDisposing)
		{
			State = state;
			_OnDisposing = onDisposing;
		}

		protected virtual void Dispose(bool disposing)
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

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}

	internal class ConsoleLoggerScopeItem
	{
		public ConsoleLoggerScope? Scope { get; set; }
	}
}
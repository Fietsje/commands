namespace Commands
{
	/// <summary>
	/// Disposable helper that runs a provided cleanup <see cref="Action"/> when disposed.
	/// </summary>
	/// <param name="cleanup">The action to invoke when the instance is disposed. May be <c>null</c>.</param>
	internal sealed class CommandUnresolver(Action cleanup) : IDisposable
	{
		private bool disposedValue;

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
		/// </value>
		public bool IsDisposed => disposedValue;

		/// <summary>
		/// Releases resources used by the instance.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> when called from <see cref="Dispose()"/> to indicate managed resources should be released;
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
				cleanup?.Invoke();
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

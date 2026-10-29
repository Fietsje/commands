using System;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Logging
{
	internal class AggregatedDisposable : IDisposable
	{
		private readonly IList<IDisposable> _disposables = new List<IDisposable>();
		public AggregatedDisposable(IEnumerable<IDisposable> disposables)
		{
			_disposables = disposables.ToList();
		}
		public void Dispose()
		{
			foreach (var disposable in _disposables)
			{
				disposable.Dispose();
			}
		}
	}
}

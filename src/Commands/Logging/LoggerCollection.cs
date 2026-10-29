using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Logging
{
	internal class LoggerCollection : ILogger, IReadOnlyCollection<ILogger>
	{
		private readonly IList<ILogger> _loggers = [];
		public int Count => _loggers.Count;

		public IEnumerator<ILogger> GetEnumerator()
		{
			return _loggers.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _loggers.GetEnumerator();
		}

		public LoggerCollection(IEnumerable<ILogger> loggers)
		{
			_loggers = loggers.ToList();
		}

		public LoggerCollection(params ILogger[] loggers)
		{
			_loggers = loggers.ToList();
		}

		internal void AddLogger(ILogger logger)
		{
			ArgumentNullException.ThrowIfNull(logger);
			_loggers.Add(logger);
		}

		public IDisposable BeginScope<TState>(TState state) where TState : notnull
		{
			AggregatedDisposable disposable = new AggregatedDisposable(_loggers.Select(l => l.BeginScope(state)));
			return disposable;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return _loggers.Any(l => l.IsEnabled(logLevel));
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			foreach (var item in _loggers)
			{
				item.Log(logLevel, eventId, state, exception, formatter);
			}
		}
	}
}

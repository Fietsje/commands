using Commands.Logging;
using CommandsTests.Commands;
using Microsoft.Extensions.Logging;

namespace Commands.Tests
{
	[TestClass()]
	public class CommandContextTests
	{
		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithNoCommand_ThenNullReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.Execute<ICommand>(null);

			Assert.IsNull(result);
			Assert.IsTrue(logger.Messages.Any(m => m.LogLevel == LogLevel.Warning), "Expected a warning message.");
		}


		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommand_ThenCommandReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new CommandThatDoesNothing();

			// Act
			var result = context.Execute<ICommand>(command);

			Assert.AreEqual(command, result);
			Assert.IsFalse(logger.Messages.Any(m => m.LogLevel == LogLevel.Warning), "Expected no warning message.");
		}



		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatThrowsException_ThenCommandExecutionFailed()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new CommandThatThrowsException();
			ICommand? result = null;

			// Act
			Action act = () => result = context.Execute<ICommand>(command);

			Assert.ThrowsException<InvalidOperationException>(act, "Expected an exception to be thrown during command execution.");
		}

		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatCallsOtherCommand_ThenCommandReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new CommandThatUsesOtherCommands();

			// Act
			var result = context.Execute<ICommand>(command);

			Assert.AreEqual(command, result);
			Assert.IsFalse(logger.Messages.Any(m => m.LogLevel == LogLevel.Warning), "Expected no warning message.");
		}

		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatFailsValidation_ThenCommandExecutionStopped()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new CommandThatFailsValidation();

			// Act
			var result = context.Execute<ICommand>(command);

			Assert.AreEqual(command, result);
			Assert.IsTrue(logger.Messages.Any(m => m.LogLevel == LogLevel.Warning), "Expected a warning message.");
		}
	}
}
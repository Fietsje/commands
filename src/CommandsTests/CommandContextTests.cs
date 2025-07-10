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

		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatNeedsValidation_ThenCommandExecutionStopped()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new DiagnosticCommandThatUsesValidation();

			// Act
			var result = context.Execute(command);

			Assert.AreEqual(command, result);
			Assert.IsTrue(result!.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsFalse(result!.ExecuteCalled, "Expected Execute to not be called on the command.");
		}

		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatNeedsDeepValidation_ThenCommandExecutionStopped()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new DiagnosticCommandWithDeepValidation { Measurement = new ValidatedObject { } };

			// Act
			var result = context.Execute(command);

			Assert.AreEqual(command, result);
			Assert.IsTrue(result!.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsFalse(result!.ExecuteCalled, "Expected Execute to not be called on the command.");
		}

		[TestCategory("CommandContext")]
		[TestMethod()]
		public void Execute_WithCommandThatNeedsDeepValidation_ThenCommandExecutionCompleted()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);
			var command = new DiagnosticCommandWithDeepValidation { Measurement = new ValidatedObject { Value = "123", Start = 1, End = 2 } };

			// Act
			var result = context.Execute(command);

			Assert.AreEqual(command, result);
			Assert.IsTrue(result!.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsTrue(result!.ExecuteCalled, "Expected Execute to be called on the command.");

			Assert.AreEqual("123", command.Measurement!.Value, "Expected Measurement.Value to be '123'.");
			Assert.AreEqual(1, command.Measurement!.Start, "Expected Measurement.Start to be 1.");
			Assert.AreEqual(2, command.Measurement!.End, "Expected Measurement.End to be 2.");
		}
	}
}
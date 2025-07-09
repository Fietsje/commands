using Commands;
using Commands.Logging;
using CommandsTests.Commands;
using Microsoft.Extensions.Logging;

namespace CommandsTests
{
	[TestClass]
	public class CommandContextExtensionsTests
	{
		[TestCategory("CommandContextExtensions")]
		[TestMethod()]
		public void Build_WithConfiguration_ThenConfiguredExecutedAndReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.Build<DiagnosticCommand>().Configure(cfg => cfg.MyProperty = 123).Execute();

			Assert.IsNotNull(result);
			Assert.IsTrue(result.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsTrue(result.ExecuteCalled, "Expected Execute to be called on the command.");
			Assert.AreEqual(123, result.MyProperty, "Expected MyProperty to be set to 123.");
		}

		[TestCategory("CommandContextExtensions")]
		[TestMethod()]
		public void Create_WithNoConfiguration_ThenCommandReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.Create<DiagnosticCommand>();

			Assert.IsNotNull(result);
			Assert.IsFalse(result.CanExecuteCalled, "Expected CanExecute to not be called on the command.");
			Assert.IsFalse(result.ExecuteCalled, "Expected Execute to not be called on the command yet.");
		}



		[TestCategory("CommandContextExtensions")]
		[TestMethod()]
		public void When_WithFalseCondition_ThenCommandNotExecutedButReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.When<DiagnosticCommand>(false).Execute();

			Assert.IsNotNull(result);
			Assert.IsFalse(result.CanExecuteCalled, "Expected CanExecute to not be called on the command.");
			Assert.IsFalse(result.ExecuteCalled, "Expected Execute to not be called on the command yet.");
		}

		[TestCategory("CommandContextExtensions")]
		[TestMethod()]
		public void When_WithTrueCondition_ThenCommandExecutedAndReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.When<DiagnosticCommand>(true).Execute();

			Assert.IsNotNull(result);
			Assert.IsTrue(result.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsTrue(result.ExecuteCalled, "Expected Execute to be called on the command.");
		}


		[TestCategory("CommandContextExtensions")]
		[TestMethod()]
		public void When_WithTrueConditionAndConfiguration_ThenCommandExecutedAndReturned()
		{
			// Arrange
			var logger = new ConsoleLogger(LogLevel.Trace);
			var resolver = new CommandResolver();
			var context = new CommandContext(logger, resolver);

			// Act
			var result = context.When<DiagnosticCommand>(true).Configure(cfg => cfg.MyProperty = 123).Execute();

			Assert.IsNotNull(result);
			Assert.IsTrue(result.CanExecuteCalled, "Expected CanExecute to be called on the command.");
			Assert.IsTrue(result.ExecuteCalled, "Expected Execute to be called on the command.");
			Assert.AreEqual(123, result.MyProperty, "Expected MyProperty to be set to 123.");
		}
	}
}

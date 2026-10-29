using Microsoft.Extensions.Logging;

namespace CommandsTests;

[TestClass]
public class LoggerTests : BaseTest
{
	[TestMethod]
	[TestCategory("Logging")]
	public void Log_WithScope_ThenScopeLogged()
	{
		// Arrange
		logger.BeginScope(new Dictionary<string, object> { ["key"] = "value", ["anotherKey"] = "anotherValue" });

		// Act
		logger.LogInformation("Test message");

		// Assert
		Assert.IsTrue(logger.Messages.Any(m => m.LogLevel == LogLevel.Information && m.Message.Contains("Test message")), "Expected an information message.");
	}
}

using Commands;
using Commands.Diagnostics;
using Commands.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommandsTests
{
	[TestClass]
	public class BaseTest
	{
		protected static TestContext testContext;
		protected static IServiceProvider serviceProvider;
		protected static ICommandContext commandContext;
		protected static ConsoleLogger logger;
		protected static int counter = 0;
		private static IDisposable? scope;

		[TestInitialize]
		public void TestInitialize()
		{
			// Perform any necessary setup before each test
			counter++;
			logger = serviceProvider.GetService<ILogger>() as ConsoleLogger;
			commandContext = serviceProvider.GetService<ICommandContext>();
			scope = logger.BeginScope(new Dictionary<string, object> { ["TestId"] = counter });
		}

		[TestCleanup]
		public void TestCleanup()
		{
			// Perform any necessary cleanup after each test
			scope?.Dispose();
		}

		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext context)
		{
			// Perform any necessary setup before any tests in the assembly are run
			testContext = context;

			serviceProvider = new ServiceCollection()
				.AddSingleton<ILogger>(_ => new ConsoleLogger(LogLevel.Trace))
				.AddTransient<ICommandResolver, DiagnosticCommandResolver>()
				.AddTransient<ICommandExecutor, DiagnosticCommandExecutor>()
				.AddTransient<ICommandContext, CommandContext>()
				.BuildServiceProvider();
		}
	}
}

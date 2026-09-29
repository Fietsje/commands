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
		protected IServiceProvider serviceProvider;
		protected ICommandContext commandContext;
		protected ConsoleLogger logger;

		[TestInitialize]
		public void TestInitialize()
		{
			// Perform any necessary setup before each test
			logger = new ConsoleLogger(LogLevel.Trace);

			serviceProvider = new ServiceCollection()
				.AddSingleton<ILogger>(_ => logger)
				.AddTransient<ICommandResolver, DiagnosticCommandResolver>()
				.AddTransient<ICommandExecutor, DiagnosticCommandExecutor>()
				.AddTransient<ICommandContext>(provider => new CommandContext(provider))
				.BuildServiceProvider();

			commandContext = serviceProvider.GetService<ICommandContext>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			// Perform any necessary cleanup after each test
		}

		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext context)
		{
			// Perform any necessary setup before any tests in the assembly are run
			testContext = context;
		}
	}
}

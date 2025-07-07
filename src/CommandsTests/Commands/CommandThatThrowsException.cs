using Commands;

namespace CommandsTests.Commands
{
	internal class CommandThatThrowsException : Command
	{
		public override string? Name => nameof(CommandThatThrowsException);
		public override void Execute(ICommandContext commandContext)
		{
			throw new InvalidOperationException("This command intentionally throws an exception.");
		}
	}
}

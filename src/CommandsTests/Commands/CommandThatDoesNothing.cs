using Commands;

namespace CommandsTests.Commands
{
	internal class CommandThatDoesNothing : Command
	{
		public override string? Name => nameof(CommandThatDoesNothing);

		public override void Execute(ICommandContext commandContext)
		{
			// This command intentionally does nothing.
		}
	}
}

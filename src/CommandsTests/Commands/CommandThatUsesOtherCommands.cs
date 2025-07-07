using Commands;
using Microsoft.Extensions.Logging;

namespace CommandsTests.Commands
{
	internal class CommandThatUsesOtherCommands : Command
	{
		public override string? Name => "Command that uses other commands";

		public override void Execute(ICommandContext commandContext)
		{
			base.Execute(commandContext);

			commandContext.Logger.LogDebug(CommandEventIds.Debug, "Executing {CommandName}", nameof(CommandThatDoesNothing));
			commandContext.Execute(new CommandThatDoesNothing());
		}
	}
}

using Commands;

namespace CommandsTests.Commands
{
	internal class CommandThatFailsValidation : Command
	{
		public override string? Name => nameof(CommandThatFailsValidation);

		public override bool CanExecute(ICommandContext commandContext)
		{
			if (!base.CanExecute(commandContext))
			{
				return false;
			}

			// Simulate a validation failure
			Status = CommandStatus.ValidationError;
			ExceptionMessage = "Validation failed for the command.";
			ExceptionNumber = 1001;

			return Status == CommandStatus.Ok;
		}
	}
}

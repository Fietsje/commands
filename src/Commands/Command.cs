using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Commands
{
	public abstract class Command : ICommand
	{
		public virtual string? Name { get; }
		public string? ExceptionMessage { get; protected set; }
		public int ExceptionNumber { get; protected set; }
		public CommandStatus Status { get; protected set; } = CommandStatus.Ok;

		public virtual bool CanExecute(ICommandContext commandContext)
		{
			if (commandContext is null)
			{
				ExceptionMessage = string.IsNullOrEmpty(Name) ? "CommandContext is null" : $"CommandContext in {Name} is null";
				ExceptionNumber = CommandEventIds.ValidationErrorOcurred.Id;
				Status = CommandStatus.ValidationError;
			}

			if (Status == CommandStatus.Ok)
			{
				ValidateCommand();
			}

			return Status == CommandStatus.Ok;
		}

		public virtual void Execute(ICommandContext commandContext)
		{
			/* This method intentionally left empty */
		}


		protected void Cancel()
		{
			Status = CommandStatus.Cancelled;
		}

		private void ValidateCommand()
		{
			Collection<ValidationResult> validationResults = [];
			Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);

			CompositeValidationResult? compositeResult = validationResults.OfType<CompositeValidationResult>().FirstOrDefault();

			string? validationMessage =
				compositeResult?.Results?.FirstOrDefault()?.ErrorMessage ??
				validationResults.FirstOrDefault()?.ErrorMessage;

			if (!string.IsNullOrEmpty(validationMessage))
			{
				ExceptionMessage = validationMessage;
				ExceptionNumber = CommandEventIds.ValidationErrorOcurred.Id;
				Status = CommandStatus.ValidationError;
			}
		}

	}
}

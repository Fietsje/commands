using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Commands
{
	/// <summary>
	/// Base implementation of <see cref="ICommand"/> that provides common execution lifecycle support,
	/// validation and status management for derived command types.
	/// </summary>
	public abstract class Command : ICommand
	{
		/// <summary>
		/// Gets the optional name of the command. Derived types may override to provide a meaningful name.
		/// </summary>
		public virtual string? Name { get; }

		/// <summary>
		/// Gets a human readable message describing an exception that occurred during execution, if any.
		/// </summary>
		public string? ExceptionMessage { get; protected set; }

		/// <summary>
		/// Gets an implementation-defined exception code associated with an error that occurred during execution.
		/// </summary>
		public int ExceptionNumber { get; protected set; }

		/// <summary>
		/// Gets the current <see cref="CommandStatus"/> for the command.
		/// </summary>
		public CommandStatus Status { get; protected set; } = CommandStatus.Ok;

		/// <summary>
		/// Determines whether the command can be executed in the provided <paramref name="commandContext"/>.
		/// Performs basic null-checking and runs <see cref="ValidateCommand"/> when the status is <see cref="CommandStatus.Ok"/>.
		/// </summary>
		/// <param name="commandContext">The context in which the command will execute. This parameter must not be <c>null</c>.</param>
		/// <returns><c>true</c> when the command is valid and can execute; otherwise <c>false</c>.</returns>
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

		/// <summary>
		/// Executes the command using the supplied <paramref name="commandContext"/>.
		/// Derived types should override this method to implement command behavior.
		/// </summary>
		/// <param name="commandContext">The context to use during execution.</param>
		public virtual void Execute(ICommandContext commandContext)
		{
			/* This method intentionally left empty */
		}


		/// <summary>
		/// Cancels the command and sets the <see cref="Status"/> to <see cref="CommandStatus.Cancelled"/>.
		/// </summary>
		protected void Cancel()
		{
			Status = CommandStatus.Cancelled;
		}

		/// <summary>
		/// Validates the current command instance using <see cref="Validator.TryValidateObject"/>.
		/// When validation errors are found the <see cref="ExceptionMessage"/>, <see cref="ExceptionNumber"/>
		/// and <see cref="Status"/> are updated accordingly.
		/// </summary>
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

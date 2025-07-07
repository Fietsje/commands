namespace System.ComponentModel.DataAnnotations
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="ValidationAttribute" />
	public class ValidateObjectAttribute : ValidationAttribute
	{
		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		/// <returns>
		/// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
		/// </returns>
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			ArgumentNullException.ThrowIfNull(value, nameof(value));

			var results = new List<ValidationResult>();
			var context = new ValidationContext(value, null, null);

			Validator.TryValidateObject(value, context, results, true);

			if (results.Count != 0)
			{
				var compositeResults = new CompositeValidationResult(String.Format(ErrorMessage ?? "Validation for {0} failed!", validationContext.DisplayName));
				results.ForEach(compositeResults.AddResult);

				return compositeResults;
			}

			return ValidationResult.Success;
		}
	}

}

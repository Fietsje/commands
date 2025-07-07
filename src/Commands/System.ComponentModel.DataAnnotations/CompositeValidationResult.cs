namespace System.ComponentModel.DataAnnotations
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="ValidationResult" />
	public class CompositeValidationResult : ValidationResult
	{
		private readonly List<ValidationResult> _results = [];

		/// <summary>
		/// Gets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		public IEnumerable<ValidationResult> Results
		{
			get
			{
				return _results;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeValidationResult"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public CompositeValidationResult(string errorMessage) : base(errorMessage) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeValidationResult"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="memberNames">The list of member names that have validation errors.</param>
		public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeValidationResult"/> class.
		/// </summary>
		/// <param name="validationResult">The validation result object.</param>
		protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

		/// <summary>
		/// Adds the result.
		/// </summary>
		/// <param name="validationResult">The validation result.</param>
		public void AddResult(ValidationResult validationResult)
		{
			_results.Add(validationResult);
		}
	}

}

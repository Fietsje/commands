using System.ComponentModel.DataAnnotations;
using Commands.Diagnostics;

namespace CommandsTests.Commands
{
	internal class DiagnosticCommandWithDeepValidation : DiagnosticCommand
	{
		[ValidateObject][Required] public ValidatedObject? Measurement { get; set; }
	}

	public class ValidatedObject
	{
		public string? Value { get; set; }
		[Required] public int? Start { get; set; }
		[Required] public int? End { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;
using Commands.Diagnostics;

namespace CommandsTests.Commands
{
	internal class DiagnosticCommandThatUsesValidation : DiagnosticCommand
	{
		public override string? Name => "Diagnostic command that uses validation for testing";

		[Required(ErrorMessage = $"{nameof(MyProperty1)} is Required")]
		public string? MyProperty1 { get; set; }
	}
}

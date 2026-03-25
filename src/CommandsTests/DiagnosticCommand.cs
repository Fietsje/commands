namespace Commands.Diagnostics
{
	public class DiagnosticCommand : Command
	{
		public override string? Name => "Diagnostic command for testing";
		public bool CanExecuteCalled { get; private set; } = false;
		public bool ExecuteCalled { get; private set; } = false;
		public int MyProperty { get; set; }

		public override bool CanExecute(ICommandContext commandContext)
		{
			CanExecuteCalled = true;
			return base.CanExecute(commandContext);
		}

		public override void Execute(ICommandContext commandContext)
		{
			ExecuteCalled = true;
			base.Execute(commandContext);
		}

	}
}

namespace Commands.Diagnostics
{
	public class DiagnosticCommandResolver : CommandResolver
	{
		public Action<Type>? OnResolving { get; set; }
		public Action<ICommand>? OnResolved { get; set; }

		public override TCommand Resolve<TCommand>()
		{
			OnResolving?.Invoke(typeof(TCommand));
			TCommand command = base.Resolve<TCommand>();

			OnResolved?.Invoke(command);
			return command;
		}
	}
}

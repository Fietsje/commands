namespace Commands.Diagnostics
{
	/// <summary>
	/// A diagnostic wrapper around <see cref="CommandResolver"/> that invokes callbacks when a command type is being resolved
	/// and after a command instance has been created.
	/// </summary>
	public class DiagnosticCommandResolver : CommandResolver
	{
		/// <summary>
		/// Optional callback invoked before a command of the specified <see cref="Type"/> is resolved.
		/// </summary>
		public Action<Type>? OnResolving { get; set; }

		/// <summary>
		/// Optional callback invoked after a command instance has been resolved.
		/// </summary>
		public Action<ICommand>? OnResolved { get; set; }

		/// <summary>
		/// Resolves a new instance of <typeparamref name="TCommand"/>, invoking <see cref="OnResolving"/>
		/// prior to resolution and <see cref="OnResolved"/> after the instance has been created.
		/// </summary>
		/// <typeparam name="TCommand">The concrete command type to resolve. Must implement <see cref="ICommand"/> and have a parameterless constructor.</typeparam>
		/// <returns>The resolved command instance.</returns>
		public override TCommand Resolve<TCommand>()
		{
			OnResolving?.Invoke(typeof(TCommand));
			TCommand command = base.Resolve<TCommand>();

			OnResolved?.Invoke(command);
			return command;
		}
	}
}

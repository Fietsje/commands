namespace Commands
{
	public class Argument<T>
	{
		public T Value { get; private set; }

		public Argument(T value)
		{
			Value = value;
		}

		public static implicit operator Argument<T>(T value) => new Argument<T>(value);
		public static implicit operator T(Argument<T> argument) => argument.Value;
	}

	public class InArgument<T> : Argument<T>
	{
		public InArgument(T value) : base(value)
		{
		}

		public static implicit operator InArgument<T>(T value) => new InArgument<T>(value);
		public static implicit operator T(InArgument<T> argument) => argument.Value;
	}

	public class OutArgument<T> : Argument<T>
	{
		public OutArgument(T value) : base(value)
		{
		}

		public static implicit operator OutArgument<T>(T value) => new OutArgument<T>(value);
		public static implicit operator T(OutArgument<T> argument) => argument.Value;
	}
}

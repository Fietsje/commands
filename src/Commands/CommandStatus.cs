namespace Commands
{
	public sealed class CommandStatus : IEquatable<CommandStatus>
	{

		/// <summary>
		/// Gets the statuscode of the commandstatus.
		/// </summary>
		public int StatusCode { get; private set; }

		/// <summary>
		/// Gets the description of the commandstatus.
		/// </summary>
		public string? Description { get; private set; }


		/// <summary>
		/// Creates a new <c>CommandStatus</c> instance stating that the command does not have a status yet.
		/// </summary>
		public static CommandStatus NotSet => new CommandStatus { StatusCode = 0, Description = "Not Set" };

		/// <summary>
		/// Creates a new <c>CommandStatus</c> instance stating that the command is ready for execution.
		/// </summary>
		public static CommandStatus Ok => new CommandStatus { StatusCode = 1, Description = "Ok" };

		/// <summary>
		/// Creates a new <c>CommandStatus</c> instance stating that the command has been cancelled.
		/// </summary>
		public static CommandStatus Cancelled => new CommandStatus { StatusCode = 101, Description = "Cancelled" };

		/// <summary>
		/// Creates a new <c>CommandStatus</c> instance stating that a validation error has ocurred.
		/// </summary>
		public static CommandStatus ValidationError => new CommandStatus { StatusCode = 102, Description = "Validation Error" };

		/// <summary>
		/// Prevents a default instance of the <see cref="CommandStatus"/> class from being created.
		/// </summary>
		private CommandStatus()
		{
			// Private constructor to prevent instantiation
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandStatus"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="description">The description.</param>
		public CommandStatus(int errorCode, string description)
		{
			StatusCode = errorCode;
			Description = description;
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object? obj)
		{
			return Equals(obj as CommandStatus);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(CommandStatus? other)
		{
			if (other is null)
			{
				return false;
			}

			return StatusCode == other.StatusCode;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return StatusCode.GetHashCode();
		}

		public static bool operator ==(CommandStatus? left, CommandStatus? right)
		{
			if (left is null)
			{
				return right is null;
			}
			return left.Equals(right);
		}

		public static bool operator !=(CommandStatus? left, CommandStatus? right)
		{
			return !(left == right);
		}
	}
}

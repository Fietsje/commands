using System.Text;

namespace Commands
{
	/// <summary>
	/// Provides utilities to produce a human-friendly name for a <see cref="Type"/>,
	/// including readable formatting for generic types (e.g. "Dictionary&lt;string,int&gt;").
	/// </summary>
	public static class FriendlyName
	{
		/// <summary>
		/// Gets a human-friendly name for the type parameter <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type to produce a friendly name for.</typeparam>
		/// <returns>
		/// A string containing the friendly name for <typeparamref name="T"/>. Generic arguments
		/// are rendered inside angle brackets and separated by commas.
		/// </returns>
		public static string GetFriendlyName<T>()
		{
			return GetFriendlyName(typeof(T));
		}

		/// <summary>
		/// Gets a human-friendly name for the specified <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> to produce a friendly name for.</param>
		/// <returns>
		/// A string containing the friendly name for <paramref name="type"/>. For generic types,
		/// the generic type definition name is used with its generic arguments rendered inside
		/// angle brackets (for example: <c>List&lt;String&gt;</c>).
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
		public static string GetFriendlyName(Type type)
		{
			if (type is null)
			{
				throw new ArgumentNullException(nameof(type), "Type cannot be null");
			}

			if (type.IsGenericType)
			{
				StringBuilder stringBuilder = new();
				stringBuilder.Append(type.Name.Split('`').FirstOrDefault());
				stringBuilder.Append('<');
				Type[] arguments = type.GetGenericArguments();

				for (int i = 0; i < arguments.Length; i++)
				{
					stringBuilder.Append(GetFriendlyName(arguments[i]));
					if (i < arguments.Length - 1)
					{
						stringBuilder.Append(',');
					}
				}

				stringBuilder.Append('>');

				return stringBuilder.ToString();
			}
			else
			{
				return type.Name;
			}
		}
	}
}

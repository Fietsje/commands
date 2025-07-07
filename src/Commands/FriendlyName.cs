using System.Text;

namespace Commands
{
	public static class FriendlyName
	{
		public static string GetFriendlyName<T>()
		{
			return GetFriendlyName(typeof(T));
		}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotelaanbieding_be.Helper
{
	/// <summary>
	///    A utility class used for verifying contracts.
	/// </summary>
	public static class Verify
	{

		/// <summary>
		///    Checks that the specified string is not null or empty.
		///    A null string is not empty.
		/// </summary>
		/// <param name="name">
		///    Required name of the string that is being checked.
		/// </param>
		/// <param name="value">
		///    Required instance of String that should not be empty.
		/// </param>
		public static String NotEmpty(string name, string value)
		{
			if (name == null)
			{
				return "code 1: name is null";
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				return "code 2: name is Empty";
			}

			if (value == null)
			{
				return "code 3: value is null";
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				return "code 4: Value is Empty";
			}

			return value;
		}

		/// <summary>
		///    Checks that the specified object is not null.
		/// </summary>
		/// <param name="name">
		///    Required name of the variable that is being checked.
		/// </param>
		/// <param name="value">
		///    Required object that should not be null.
		/// </param>
		public static object NotNull(string name, object value)
		{
			if (name == null)
			{
				return "code no: name is null"; ;
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				return "code no: name is Empty"; ;
			}

			if (value == null)
			{
				return "code no: values is null"; ;
			}

			return value;
		}
	}
}

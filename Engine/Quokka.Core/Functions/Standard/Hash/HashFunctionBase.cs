using System;
using System.Security.Cryptography;
using System.Text;

namespace Mindbox.Quokka
{
	internal abstract class HashFunctionBase : ScalarTemplateFunction<string, string>
	{
		protected abstract HashAlgorithm CreateHashAlgorithm();

		public HashFunctionBase(string name)
			: base(
				  name,
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return GetHashString(argument1);
		}

		private string GetHashString(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			using (var hashAlgorithm = CreateHashAlgorithm())
			{
				var plainBytes = Encoding.UTF8.GetBytes(value);
				var encodedBytes = hashAlgorithm.ComputeHash(plainBytes);
				return ByteUtility.ToHexString(encodedBytes);
			}
		}
	}
}

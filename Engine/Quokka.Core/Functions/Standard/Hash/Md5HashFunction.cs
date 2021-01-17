using System.Security.Cryptography;

namespace Mindbox.Quokka
{
	internal class Md5HashFunction : HashFunctionBase
	{
		public Md5HashFunction() : base("md5")
		{
		}

		protected override HashAlgorithm CreateHashAlgorithm() => MD5CryptoServiceProvider.Create();
	}
}

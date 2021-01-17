using System.Security.Cryptography;

namespace Mindbox.Quokka
{
	internal class Sha512HashFunction : HashFunctionBase
	{
		public Sha512HashFunction() : base("sha512")
		{
		}

		protected override HashAlgorithm CreateHashAlgorithm() => SHA512.Create();
	}
}

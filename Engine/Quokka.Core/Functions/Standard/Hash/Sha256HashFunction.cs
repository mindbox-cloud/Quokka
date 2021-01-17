using System;
using System.Security.Cryptography;
using System.Text;

namespace Mindbox.Quokka
{
	internal class Sha256HashFunction : HashFunctionBase
	{
		public Sha256HashFunction() : base("sha256")
		{
		}

		protected override HashAlgorithm CreateHashAlgorithm() => SHA256.Create();
	}
}

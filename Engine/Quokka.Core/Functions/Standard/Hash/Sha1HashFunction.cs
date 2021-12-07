using System;
using System.Security.Cryptography;
using System.Text;

namespace Mindbox.Quokka
{
	internal class Sha1HashFunction : HashFunctionBase
	{
		public Sha1HashFunction() : base("sha1")
		{
		}

		protected override HashAlgorithm CreateHashAlgorithm() => SHA1.Create();
	}
}

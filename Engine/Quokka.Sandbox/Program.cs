using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

using Quokka.Generated;

namespace Quokka.Sandbox
{
	public class Program
	{
		private static Template ParseFileContents(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("string.IsNullOrEmpty(filePath)", nameof(filePath));

			return new Template(File.ReadAllText(filePath));
		}

		static void Main(string[] args)
		{
			var template = ParseFileContents(@"c:\Code\Quokka\Grammar\Quokka\sample inputs\100. Temp 1.txt");

			foreach (var debugMessage in template.GetDebugMessages())
				Console.WriteLine(debugMessage);

			Console.WriteLine("Press Enter to exit:");
			Console.ReadLine();
		}
	}
}

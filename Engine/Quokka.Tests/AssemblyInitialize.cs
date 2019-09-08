using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests 
{
    [TestClass]
    public class Initialization 
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}
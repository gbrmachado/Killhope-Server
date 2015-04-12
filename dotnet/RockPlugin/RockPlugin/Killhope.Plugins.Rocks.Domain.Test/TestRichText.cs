using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestRichText
    {
        private const string testFormattedData = "<u>Hello World</u>";
        //having two opening tags nested 
        private const string invalidData = "<u>Hello<u> World</u></u>";

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorFailsOnNull()
        {
            new RichText(null);
        }

        [TestMethod]
        public void BlankConsturctorWorks()
        {
            new RichText();
        }

        [TestMethod]
        public void NormalConstuctorSetsValueCorrectlyRegardlessOfFormatting()
        {
            var rich = new RichText(testFormattedData);
            Assert.AreEqual("Hello World", rich.Value);

            Assert.AreEqual(1, rich.UnderlineLocations.Count());
            var pos = rich.UnderlineLocations.First();
            Assert.AreEqual(0, pos.Start);
            Assert.AreEqual(11, pos.End);

        }



    }
}

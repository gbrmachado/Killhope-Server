using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Rocks.Domain.Formatting;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestSingleTag
    {
        private static readonly SingleHTMLTag br = new SingleHTMLTag("br");

        [TestMethod]
        public void ConstructorWorksWithTag()
        {
            var tag = new SingleHTMLTag("br");
            Assert.AreEqual("br", tag.TagName);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorFailsOnNull()
        {
            new SingleHTMLTag(null);
        }

        [TestMethod]
        public void ProcessRemovesTag()
        {
            string toTest = "aa<br/>aa";
            var value = br.Process(toTest, new Position(2,7));
            Assert.AreEqual("aaaa", value);
        }

        [TestMethod]
        public void PositionIsDeterminedCorrectly()
        {
            string toTest = "aa<br/>aa";
            var value = br.GetAllPositons(toTest);
            Assert.AreEqual(1, value.Count());
            var pos = value.First();
            Assert.AreEqual(2, pos.Start);
            Assert.AreEqual(7, pos.End);
        }
    }
}

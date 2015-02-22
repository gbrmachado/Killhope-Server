using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Rocks.Domain.Formatting;
using System.Collections.Generic;
using System.Linq;
using Killhope.Plugins.Rocks.Domain.Formatting.Exceptions;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestHTMLDualTagFormatting
    {
        private static HTMLDualTagFormatting GetUTag()
        {
            return GetTag("u");
        }

        private static HTMLDualTagFormatting GetTag(string name)
        {
            return new HTMLDualTagFormatting(name);
        }

        [TestMethod]
        public void TestGetPositionsOnValidInput()
        {
            TextFormattingRule uTag = GetUTag();
            string valid = "HelloWorld";
            string input = String.Format("<u>{0}</u>", valid);
            List<Position> positions = uTag.GetAllPositons(input).ToList();

            Assert.AreEqual(1, positions.Count);
            Assert.AreEqual(valid, positions[0].Substring(input));
        }

        [TestMethod]
        public void TestTwoValidHTMLTags()
        {
            TextFormattingRule uTag = GetUTag();
            string valid = "HelloWorld";
            string valid2 = "FooBarBaz";
            string input = String.Format("<u>{0}</u> AA <u>{1}</u>", valid, valid2);
            List<Position> positions = uTag.GetAllPositons(input).ToList();

            Assert.AreEqual(2, positions.Count);
            Assert.AreEqual(valid, positions[0].Substring(input));
            Assert.AreEqual(valid2, positions[1].Substring(input));
        }

        [TestMethod]
        public void Process()
        {
            TextFormattingRule uTag = GetUTag();
            string valid = "HelloWorld";
            string input = String.Format("<u>{0}</u>", valid);
            List<Position> positions = uTag.GetAllPositons(input).ToList();
            Assert.AreEqual(valid, uTag.Process(input, positions[0]));
        }

        [TestMethod]
        public void ProcessRemovesCorrectLength()
        {
            HTMLDualTagFormatting uTag = GetUTag();
            string valid = "HelloWorld";
            string input = String.Format("<u>{0}</u>", valid);
            string processed = uTag.Process(input, uTag.GetAllPositons(input).First());
            Assert.AreEqual(input.Length, processed.Length + uTag.TotalLength);
        }

        #region "Invalid Constructs"


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullTagThrowsException()
        {
            GetTag(null);
        }
        #endregion

        [TestMethod, ExpectedException(typeof(InvalidFormatException))]
        public void CloseTagBeforeStartTagThrowsError()
        {
            var tag = GetUTag();
            tag.GetAllPositons("</u><u>").First();
        }

        /// <summary>
        /// Confirms that the first occurance that the end tag is before the start tag will throw an exception (Speical Case).
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidFormatException))]
        public void NoStartTagWithFirstCloseThrowsError()
        {
            var tag = GetUTag();
            tag.GetAllPositons("</u>").First();
        }

        [TestMethod, ExpectedException(typeof(OverlappingTagException))]
        public void DualStartTagThrowsError()
        {
            var tag = GetUTag();
            tag.GetAllPositons("<u><u></u>").First();
        }

        [TestMethod, ExpectedException(typeof(InvalidFormatException))]
        public void DualCloseTagThrowsError()
        {
            var tag = GetUTag();
            //<u></u> is valid, the trailign clsoe tag should fail.
            tag.GetAllPositons("<u></u></u>").ToList();
        }
    }
}

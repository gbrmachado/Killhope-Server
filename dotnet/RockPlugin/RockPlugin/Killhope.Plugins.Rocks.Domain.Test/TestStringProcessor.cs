using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Killhope.Plugins.Rocks.Domain.Formatting;
using System.Linq;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestStringProcessor
    {
        private HTMLDualTagFormatting u = new HTMLDualTagFormatting("u");
        private HTMLDualTagFormatting h1 = new HTMLDualTagFormatting("h1");
        private HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");
        private SingleHTMLTag br = new SingleHTMLTag("br");

        private List<TextFormattingRule> getSingleRule()
        {
            return new List<TextFormattingRule> { u };
        }

        private List<TextFormattingRule> getDoubleRule()
        {
            return new List<TextFormattingRule> { u, b };
        }

        private IEnumerable<TextFormattingRule> getSingleTag()
        {
            return new List<TextFormattingRule> { br };
        }


        private IEnumerable<TextFormattingRule> getDifferentRule()
        {
            return new List<TextFormattingRule> { u, h1 };
        }


        [TestMethod]
        public void TestNoChanges()
        {
            string res = StringProcessor.ProcessString("", getSingleRule()).Value;
            Assert.AreEqual("", res);
        }

        [TestMethod]
        public void TestSingleChange()
        {
            var res = StringProcessor.ProcessString("<u>aa</u>", getSingleRule());
            string strRes = res.Value;
            Assert.AreEqual("aa", strRes);
            Assert.AreEqual(1, res.Positions[u].Count);

        }

        [TestMethod]
        public void TestDoubleChange()
        {
            var res = StringProcessor.ProcessString("<u>aa</u>aa<u>bb</u>", getSingleRule());
            string strRes = res.Value;
            Assert.AreEqual("aaaabb", strRes);

            //TODO: Also need to check the positions in the new string.
            var pos = res.Positions[u];

            Assert.AreEqual(2, pos.Count);
            Assert.AreEqual(0, pos.First().Start);
            Assert.AreEqual(2, pos.First().Length);

            Assert.AreEqual(4, pos.Skip(1).First().Start);
            Assert.AreEqual(2, pos.Skip(1).First().Length);

        }

        [TestMethod]
        public void TestNestedTags()
        {
            var res = StringProcessor.ProcessString("<u><b>aa</b></u>", getDoubleRule());
            //in the current impl, should be:
            //<u><b>aa</b></u> [] ->
            //<b>aa</b> [(3:12)] ->
            //[-3,-3-3-4]
            //aa [(0:2),(0:2)]


            string strRes = res.Value;
            Assert.AreEqual("aa", strRes);

            //TODO: Also need to check the positions in the new string.
            var pos = res.Positions;
            var posU = res.Positions[u].First();

            var posB = res.Positions[b].First();

            Assert.AreEqual(2, pos.Count);
            //Both tags should be from 0:2 on the new string.
            Assert.AreEqual(0, posU.Start);
            Assert.AreEqual(2, posU.Length);



            Assert.AreEqual(0, posB.Start);
            Assert.AreEqual(2, posB.Length);

        }

        [TestMethod]
        public void testNonContiguousTags()
        {
            var res = StringProcessor.ProcessString("aa<u>bb<b>cc</u>dd</b>", getDoubleRule());
            Assert.AreEqual("aabbccdd", res.Value);

            var pos = res.Positions;
            var posU = res.Positions[u].First();

            var posB = res.Positions[b].First();

            Assert.AreEqual(2, pos.Count);
            Assert.AreEqual(2, posU.Start);
            Assert.AreEqual(6, posU.End);

            Assert.AreEqual(4, posB.Start);
            Assert.AreEqual(8, posB.End);


        }

        [TestMethod]
        public void DifferentLengthsNoOverlap()
        {
            var res = StringProcessor.ProcessString("aa <u> bb </u> c <h1> HEAD</h1>", getDifferentRule());
            Assert.AreEqual("aa  bb  c  HEAD", res.Value);

            var pos = res.Positions;
            var posU = res.Positions[u].First();

            var posB = res.Positions[h1].First();

            Assert.AreEqual(2, pos.Count);
            Assert.AreEqual(3, posU.Start);
            Assert.AreEqual(7, posU.End);

            Assert.AreEqual(10, posB.Start);
            Assert.AreEqual(15, posB.End);
        }

        [TestMethod]
        public void SingleTagWorksAsExpected()
        {
            var res = StringProcessor.ProcessString("aa<br/>bb", getSingleTag());
            Assert.AreEqual("aabb", res.Value);

            var pos = res.Positions;
            var brTagPosition = res.Positions[br].First();

            Assert.AreEqual(brTagPosition.Start, brTagPosition.End);
            Assert.AreEqual(2, brTagPosition.Start);
        }



        public void Bug_1_br_failure()
        {
            //Throws: ArgumnetException 
            //This is the original code that caused the bug to be produced.
            //redux will reduce the code to the minimum value needed to reproduce.
            HTMLDualTagFormatting u = new HTMLDualTagFormatting("u");
            HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");
            HTMLDualTagFormatting i = new HTMLDualTagFormatting("i");
            SingleHTMLTag br = new SingleHTMLTag("br");

            //Note that the >> was there unintentionally.
            string bug_1 = "Hi There, this is an initial Test.<br/><br/><u>Hello World</u>><br/><b>Hello World</b>><br/><i>Hello World</i>";
            var res = StringProcessor.ProcessString(bug_1, new List<TextFormattingRule> { u, b, i, br });
        }

        [TestMethod]
        public void Bug_1_br_redux()
        {


            HTMLDualTagFormatting u = new HTMLDualTagFormatting("u");
            HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");
            HTMLDualTagFormatting i = new HTMLDualTagFormatting("i");
            SingleHTMLTag br = new SingleHTMLTag("br");

            //Explanation of cause:
            //Initially: Problem was due to an instance of WrappedFormat being created over a single HTML tag with a wrapped rule inserted.
            //This should never have happened in the first place, code was modified to throw an error earlier on.

            //Once found, the Position was from 34:39 and 39:44, this was deemed by the code to be an overlap of type: OverlapType.Right
            //Unit tests were added to fix this (TestPosition_bug_1), now returns OverlapType.None
            string bug_1 = "Hi There, this is an initial Test.<br/><br/><u>Hello World</u>><br/><b>Hello World</b>><br/><i>Hello World</i>";
            var res = StringProcessor.ProcessString(bug_1, new List<TextFormattingRule> { u, b, i, br });
         
            //only need to assert that no error was created.
        }

        [TestMethod]
        public void Bug_2_br_redux()
        {
            //Same code as Bug_1, the displayed text was shown incorrectly.

            HTMLDualTagFormatting u = new HTMLDualTagFormatting("u");
            HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");
            HTMLDualTagFormatting i = new HTMLDualTagFormatting("i");
            SingleHTMLTag br = new SingleHTMLTag("br");

            //Looking a the output text, it was as expected.
            //Looking at the positioning of the breaks, it was as expected.
            //Output seemed perfect, there was a Bug using System.Windows.Forms.RichTextBox, assumed: start,end, was actually start, length

            //TODO: Before this is marked as done, add an adapter to the presentation layer.

            string bug_1 = "<br/><br/><u>Hello World</u>><br/><b>Hello World</b>><br/><i>Hello World</i>";
            var res = StringProcessor.ProcessString(bug_1, new List<TextFormattingRule> { u, b, i, br });
            string result = res.Value;
            Assert.AreEqual("Hello World>Hello World>Hello World", result);
            Assert.Inconclusive();
        }

    }
}

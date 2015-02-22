using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Rocks.Domain.Formatting;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestIncrementalStringReplacer
    {
        [TestMethod]
        public void TestNoChange()
        {
            IncrementalStringReplacer r = new IncrementalStringReplacer("aa");

            Assert.AreEqual("aa", r.Value);
        }

        [TestMethod]
        public void TestInitialBR()
        {
            IncrementalStringReplacer r = new IncrementalStringReplacer("aa");
            r.IncrementPosition(new Position(0, 0), FormatFactory.br);
            Assert.AreEqual("<br/>aa", r.Value);
        }

        [TestMethod]
        public void BRTagAtEndIsReplaced()
        {
            IncrementalStringReplacer r = new IncrementalStringReplacer("aa");
            r.IncrementPosition(new Position(2, 2), FormatFactory.br);
            Assert.AreEqual("aa<br/>", r.Value);
        }

        [TestMethod]
        public void OneDoubleTagIsHandled()
        {
            IncrementalStringReplacer r = new IncrementalStringReplacer("aa");
            r.IncrementPosition(new Position(2, 2), FormatFactory.u);
            Assert.AreEqual("aa<u></u>", r.Value);
        }


        [TestMethod]
        public void TestNonNested()
        {
            //This is going to be a massive pain.
            IncrementalStringReplacer r = new IncrementalStringReplacer("aaa");
            r.IncrementPosition(new Position(0, 2), FormatFactory.u);
            r.IncrementPosition(new Position(1, 3), FormatFactory.b);
            Assert.AreEqual("<u>a<b>a</u>a</b>", r.Value);
        }



        [TestMethod]
        public void LongString()
        {
            //This is going to be a massive pain.
            IncrementalStringReplacer r = new IncrementalStringReplacer("aaaaaa");
            r.IncrementPosition(new Position(0, 2), FormatFactory.u);
            r.IncrementPosition(new Position(2, 4), FormatFactory.u);
            r.IncrementPosition(new Position(4, 6), FormatFactory.b);
            Assert.AreEqual("<u>aa</u><u>aa</u><b>aa</b>", r.Value);
        }

        [TestMethod]
        public void TestNestingIsContinuous()
        {
            IncrementalStringReplacer r = new IncrementalStringReplacer("aa");
            r.IncrementPosition(new Position(0, 2), FormatFactory.b);
            r.IncrementPosition(new Position(0, 2), FormatFactory.u);
            string actual = r.Value;
            Assert.AreEqual("<b><u>aa</u></b>", actual);
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestAdvancedUndoRedo()
        {
            var str = "<b>Hi <u>There,</b> this</u> <b><u>is</u></b> an initial Test.<br/><br/><u>Hello World</u>><br/><b>Hello World</b>><br/><i>Hello World</i>";
            var rich = new RichText(str);
            string result = rich.ToString();
            Assert.AreEqual(str, result);
        }
    }
}

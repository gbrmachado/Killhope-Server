using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestPosition
    {
        [TestMethod]
        public void TestBlankSubstring()
        {
            Position p = new Position(0, 0);
            string s = p.Substring("aa");
            Assert.AreEqual(String.Empty, s);
        }

        [TestMethod]
        public void TestSubstring()
        {
            Position p = new Position(1, 2);
            string s = p.Substring("ab");
            Assert.AreEqual("b", s);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void LessThanZeroThrowsError()
        {
            Position p = new Position(-1, 0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EndLessThanStartThrowsError()
        {
            Position p = new Position(1, 0);
        }

        //Testing Overlapping.

        [TestMethod]
        public void NonOverlapWorks()
        {
            Assert.AreEqual(Position.OverlapType.None, new Position(0,0).GetOverlap(new Position(1,1)));
        }

        [TestMethod]
        public void OverlapWorks()
        {
            Assert.AreEqual(Position.OverlapType.Total, new Position(0, 9).GetOverlap(new Position(5, 8)));
        }

        [TestMethod]
        public void PartialOverlapWorks()
        {
            Assert.AreEqual(Position.OverlapType.Right, new Position(5, 12).GetOverlap(new Position(10, 18)));
        }

        [TestMethod]
        public void bug_1_regression()
        {
            //If the End position of a comparison was equal to the start of the next, then .Right was returned instead of .None
            //From, Bug 1 in TestStringProcessor.
            Assert.AreEqual(Position.OverlapType.None, new Position(34, 39).GetOverlap(new Position(39, 44)));
        }
        //TODO: Test Invalid substring
    }
}

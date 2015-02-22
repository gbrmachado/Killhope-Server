using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Rocks.Domain.Formatting;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestWrappedFormat
    {
        [TestMethod]
        public void TestNormalWrap()
        {
            HTMLDualTagFormatting u = new HTMLDualTagFormatting("uu");
            HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");

            string toTest = "<uu><b>aa</b></uu>";

            WrappedFormat r = new WrappedFormat(u, new Position(4, 13));
            r.AddRule(b, Position.OverlapType.Total);


            Position mod = r.ModifiedPosition;
            Assert.AreEqual(0, mod.Start);

            Assert.AreEqual(2, mod.End);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Presentation;
using Killhope.Plugins.Manager.Presentation.Test.Mocks;

namespace Killhope.Plugins.Manager.Test
{
    [TestClass]
    public class TestMainForm
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Form1FailsWithBothNull()
        {
            new Form1(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Form1FailsWithSecondNull()
        {
            new Form1(new MockFTPReleaseSide(), null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Form1FailsWithFirstNull()
        {
            new Form1(null, new MockTempSideFactory());
        }

        [TestMethod]
        public void Form1WithNonNullIsSucessful()
        {
            new Form1(new MockFTPReleaseSide(), new MockTempSideFactory());
        }

    }
}

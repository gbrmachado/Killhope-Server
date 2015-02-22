using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Presentation;

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
    }
}

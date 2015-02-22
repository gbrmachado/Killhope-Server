using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Domain.Release.DTO;

namespace Killhope.Domain.Plugins.Manager.IntTests
{
    [TestClass]
    public class TestSiteManifest
    {
        [TestMethod]
        public void ToStringDoesNotFailIfAllAreNull()
        {
            new SiteManifest().ToString();
        }

        [TestMethod]
        public void ToStringReturnsDisplayNameIfSet()
        {
            Assert.AreEqual("HelloWorld", new SiteManifest() { DisplayName = "HelloWorld" }.ToString());
        }

        [TestMethod]
        public void ToStringReturnsDisplayNameIfAllSet()
        {
            var manifest = new SiteManifest() { DisplayName = "HelloWorld", FTPPath = "aa", FTPServer = "bb", UseSFTP = false, DefaultUserName = "David" };
            Assert.AreEqual("HelloWorld", manifest.ToString());
        }

        [TestMethod]
        public void ToStringReturnsServerAndNameIfDisplayNameNotSet()
        {
            var manifest = new SiteManifest() { DisplayName = null, FTPPath = "aa", FTPServer = "ftp.dur.ac.uk", UseSFTP = false, DefaultUserName = "David" };
            Assert.AreEqual("David@ftp.dur.ac.uk", manifest.ToString());
        }

        [TestMethod]
        public void ToStringReturnsServerAndNameIfDisplayNameEmpty()
        {
            var manifest = new SiteManifest() { DisplayName = "    ", FTPPath = "aa", FTPServer = "ftp.dur.ac.uk", UseSFTP = false, DefaultUserName = "David" };
            Assert.AreEqual("David@ftp.dur.ac.uk", manifest.ToString());
        }
    }
}

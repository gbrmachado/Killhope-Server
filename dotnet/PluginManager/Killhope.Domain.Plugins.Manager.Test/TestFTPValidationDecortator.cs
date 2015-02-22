using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Domain.Release.FTP;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class TestFTPValidationDecortator
    {
        private MockFTPClient mock = new MockFTPClient();

        private FTPClientValidationDecorator getInstance()
        {
            return new FTPClientValidationDecorator(mock);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullFails()
        {
            new FTPClientValidationDecorator(null);
        }

        [TestMethod]
        public void validInstanceWorks()
        {
            getInstance();
        }

        [TestMethod]
        public void DownloadingCausesValidation()
        {
            var i = getInstance();
            mock.setAsValid();
            i.DownloadFile(MockFTPClient.ValidFileDownload);
            Assert.IsTrue(mock.ValidationWasAttempted);
        }

        [TestMethod, ExpectedException(typeof(FTPClientValidationDecorator.InvalidFTPSiteException))]
        public void InvalidCausesException()
        {
            var i = getInstance();
            i.DownloadFile(MockFTPClient.ValidFileDownload);
        }

        [TestMethod, ExpectedException(typeof(FTPClientValidationDecorator.InvalidFTPSiteException))]
        public void InvalidManifestJSONCausesException()
        {
            var i = getInstance();
            mock.InvalidateDownload();
            i.DownloadFile(MockFTPClient.ValidFileDownload);
        }
    }
}

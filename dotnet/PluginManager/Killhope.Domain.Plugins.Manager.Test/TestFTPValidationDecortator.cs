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
            mock.FolderIsCreated = true;
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

        [TestMethod]
        public void ValidationIsNotCalledTwice()
        {
            var i = getInstance();
            mock.setAsValid();
            i.DownloadFile(MockFTPClient.ValidFileDownload);
            i.DownloadFile(MockFTPClient.ValidFileDownload);
            Assert.AreEqual(1, mock.TimesValidationFileDownloaded);
        }

        [TestMethod]
        public void OnlyOneFileIsDownloadedForValidation()
        {
            var i = getInstance();
            mock.setAsValid();
            i.DownloadFile(MockFTPClient.ValidFileDownload);
            Assert.AreEqual(2, mock.TotalDownloadCalls);
        }

        [TestMethod, ExpectedException(typeof(FTPClientValidationDecorator.InvalidFTPSiteException))]
        public void HavingNoCretedFolderFails()
        {
            var i = getInstance();
            mock.setAsValid();
            mock.FolderIsCreated = false;
            i.DownloadFile(MockFTPClient.ValidFileDownload);
            Assert.Fail();
        }
    }
}

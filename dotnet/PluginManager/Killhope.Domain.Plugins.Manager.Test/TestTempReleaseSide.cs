using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain.Test.Mock;
using System.Collections.Generic;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class TestTempReleaseSide
    {
        string path;
        MockLocaReleaseManager manager;

        string FileDefaultText = "Hello World";
        private ItemLocation newFileLocation;
        private File newFile;


        private TempReleaseSide getDefault()
        {
            path = "";
            this.manager = new MockLocaReleaseManager();
            manager.Domain = "HelloWorld";
            manager.folders = new List<string> { "a" };

            return new TempReleaseSide(new LocalReleaseManager(manager, new ItemLocation("","")));
        }

        private TempReleaseSide getReleaseWithNewFile()
        {
            var release = getDefault();
            release.Add(GetNewFile());
            return release;
        }

        private File GetNewFile()
        {
            newFileLocation = new ItemLocation("aa", "bb");
            this.newFile = new MockFile(newFileLocation, FileDefaultText);
            return this.newFile;
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FailsOnNull()
        {
            new TempReleaseSide(null);
        }

        [TestMethod]
        public void CanPerformAllActions()
        {
            var release = getDefault();

            Assert.IsTrue(release.CanAdd);
            Assert.IsTrue(release.CanRemove);
            Assert.IsTrue(release.CanRefresh);
        }

        [TestMethod]
        public void AddNewFileModifiesCount()
        {
            var release = getReleaseWithNewFile();

            Assert.AreEqual(1, release.FileCount);
        }

        [TestMethod]
        public void AddNewFileWillPerformFileTransfer()
        {
            var release = getReleaseWithNewFile();

            Assert.AreEqual(1, manager.FileSavesPerformed);
            Assert.AreEqual(this.newFileLocation, manager.lastFileSaved.Location);
        }

        [TestMethod]
        public void DeletingNewFileResetsCount()
        {
            var release = getReleaseWithNewFile();

            release.Remove(this.newFile.Location);

            Assert.AreEqual(0, release.FileCount);
        }

        [TestMethod]
        public void DeletingNewFilePerformsDelete()
        {
            var release = getReleaseWithNewFile();

            release.Remove(this.newFile.Location);

            Assert.AreEqual(1, manager.DeletionsPerformed);
        }
    }
}

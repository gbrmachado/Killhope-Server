using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Domain.Release;
using System.Collections.Generic;
using System.IO;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class TestPathFunctions
    {
        [TestMethod]
        public void BuildPathNoneWorks()
        {
            string ret = LocalFileSystemManager.BuildPath("/", new List<string> { });
            Assert.AreEqual("/", ret);
        }

        [TestMethod]
        public void BuildPathEndsInSlash()
        {
            string ret = LocalFileSystemManager.BuildPath("/", new List<string> { "a"});
            Assert.AreEqual("/a" + Path.DirectorySeparatorChar, ret);
        }

        [TestMethod]
        public void MultiBuildPathEndsInSlash()
        {
            string ret = LocalFileSystemManager.BuildPath("/", new List<string> { "a", "a" });
            Assert.AreEqual("/a" + Path.DirectorySeparatorChar + "a" + Path.DirectorySeparatorChar, ret);
        }

        [TestMethod]
        public void FromRelativeFilePathDefaultTest()
        {
            ItemLocation l = ItemLocation.FromRelativeFilePath("");
            Assert.Inconclusive();
        }

    }
}

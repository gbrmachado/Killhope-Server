using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class TestMenuItem
    {
        [TestMethod]
        public void ValidConstructorWorks()
        {
            ItemLocation a = new ItemLocation("a", "b");
            Assert.AreEqual("a", a.Folders.First());
            Assert.AreEqual("b", a.ItemName);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullName()
        {
            new ItemLocation("a", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullFolder()
        {
            new ItemLocation(null, "b");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidName()
        {
            new ItemLocation("a", "b/");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidFolder()
        {
            new ItemLocation("a/", "b");
        }

        [TestMethod]
        public void AddingValidFolderDoesNotChangeName()
        {
            ItemLocation a = new ItemLocation("a", "b").AddFolder("c");
            Assert.AreEqual("b", a.ItemName);
        }

        [TestMethod]
        public void AddingValidFolderAddsFolderInSecondPlace()
        {
            ItemLocation a = new ItemLocation("a", "b").AddFolder("c");
            Assert.AreEqual("c", a.Folders.Skip(1).First());
        }

        [TestMethod]
        public void AddingValidFolderDoesNotChangeFirstFolder()
        {
            ItemLocation a = new ItemLocation("a", "b").AddFolder("c");
            Assert.AreEqual("a", a.Folders.First());
        }

        [TestMethod]
        public void AddingValidFolderOnlyAddsOneFolder()
        {
            ItemLocation a = new ItemLocation("a", "b").AddFolder("c");
            Assert.AreEqual(2, a.Folders.Count());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddingInvalidFolderThrowsException()
        {
            new ItemLocation("a", "b").AddFolder("c/");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullFolderThrowsException()
        {
            new ItemLocation("a", "b").AddFolder(null);
        }

        [TestMethod]
        public void EquivalentFoldersAreListed()
        {
            string initialFolder = "a";
            string secondFolder = "c";
            var one = new ItemLocation(initialFolder, "b").AddFolder(secondFolder);
            var two = new ItemLocation(initialFolder, "c").AddFolder(secondFolder);

            CollectionAssert.AreEquivalent(one.Folders.ToList(), two.Folders.ToList());

            var folders = one.GetFolderSimilarities(two);
            Assert.AreEqual(initialFolder, folders.First());
            Assert.AreEqual(secondFolder, folders.Skip(1).First());
        }

        [TestMethod]
        public void PartialEquivalentFoldersAreListedCorrectly()
        {
            string initialFolder = "a";
            string secondFolder = "c";
            string thirdFolder = "d";
            var one = new ItemLocation(initialFolder, "b").AddFolder(secondFolder).AddFolder(thirdFolder);
            var two = new ItemLocation(initialFolder, "c").AddFolder(secondFolder);

            var folders = one.GetFolderSimilarities(two);

            Assert.AreEqual(initialFolder, folders.First());
            Assert.AreEqual(secondFolder, folders.Skip(1).First());

            Assert.AreEqual(2, folders.Count());
        }


        [TestMethod]
        public void FirstEquivalentFolderIsOnlyListed()
        {
            string initialFolder = "a";
            string secondFolder = "c";
            string thirdFolder = "d";
            var one = new ItemLocation(initialFolder, "b").AddFolder("invalid").AddFolder(thirdFolder);
            var two = new ItemLocation(initialFolder, "c").AddFolder(secondFolder);

            var folders = one.GetFolderSimilarities(two);

            Assert.AreEqual(initialFolder, folders.First());

            Assert.AreEqual(1, folders.Count());
        }

        [TestMethod]
        public void TestEquality()
        {
            var one = new ItemLocation("a", "b");
            var two = new ItemLocation("a", "b");

            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void ToPath()
        {
            Assert.Inconclusive();
        }

    }
}

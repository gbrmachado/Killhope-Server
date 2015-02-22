using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class MenuItemManagerTest
    {
        public const string childName = "Plugin";
        private const string folderName = "Tools";

        private int called;
        private MenuItemManager man;

        [TestInitialize]
        public void init()
        {
            man = new MenuItemManager();
        }

        private NamedMenuItem makeMenuItem(ItemLocation location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            return new NamedMenuItem(location, () => called++);
        }

        //TODO: Shorten syntax further, allow setting a folder, name, then varargs for the folder tree.
        private void AddMenuItem(ItemLocation location)
        {
            man.AddMenuItem(makeMenuItem(location));
        }



        [TestMethod]
        public void BlankConstructorThrowsNoErrors()
        {
            new MenuItemManager();
        }

        [TestMethod]
        public void OneItemHasChild()
        {  
            AddMenuItem(new ItemLocation(folderName, childName));

            Assert.AreEqual(1, man.Items.Count());
            var item = man.Items.First();
            Assert.AreEqual(item.CanExpand, true);
            Assert.AreEqual(item.Children.Count, 1);

            var plugin = item.Children.First();
            Assert.AreEqual(plugin.CanExpand, false);
            Assert.AreEqual(plugin.Name, childName);

        }



        /// <summary>
        /// Assert that having an item nested 2 layers deep won't cause any problems.
        /// </summary>
        [TestMethod]

        public void FolderTreeOfTwoWorks()
        {
            AddMenuItem(new ItemLocation(folderName, childName).AddFolder("Folder2"));

            Assert.AreEqual(1, man.Items.Count());
            var folder = man.Items.First();
            isFolder(folder, 1);
            var folder2 = folder.Children.First();
            isFolder(folder2, 1, "Folder2");


            var item = folder2.Children.First();
            Assert.AreEqual(childName, item.Name);
            Assert.AreEqual(false, item.CanExpand);
            Assert.AreEqual(0, item.Children.Count);
        }



        [TestMethod]
        public void AddTwoItemsUnderExactSameFolder()
        {
            //Both items under the "Tools" folder.

            AddMenuItem(new ItemLocation(folderName, childName));
            AddMenuItem(new ItemLocation(folderName, "2"));

            Assert.AreEqual(1, man.Items.Count());

            var sharedFolder = man.Items.First();
            isFolder(sharedFolder, 2, folderName);

            assertIsEndpoint(sharedFolder.Children.First(), childName);
            assertIsEndpoint(sharedFolder.Children.Skip(1).First(), "2");
        }

        [TestMethod]
        public void AddTwoItemsWithSimilarFolder()
        {
            //Both under different folders under tools

            AddMenuItem(new ItemLocation(folderName, childName).AddFolder("Folder2"));
            AddMenuItem(new ItemLocation(folderName, "2").AddFolder("aa"));

            Assert.AreEqual(1, man.Items.Count());

            var sharedFolder = man.Items.First();
            isFolder(sharedFolder, 2, folderName);

            //TODO: New test - assert the order is the order of added.
            var folder2 = sharedFolder.Children.First();
            isFolder(folder2, 1, "Folder2");

            var folder3 = sharedFolder.Children.Skip(1).First();
            isFolder(folder3, 1, "aa");

            assertIsEndpoint(folder2.Children.First(), childName);
            assertIsEndpoint(folder3.Children.First(), "2");

        }

        private void assertIsEndpoint(MenuItemManager.FolderItem item, string childName)
        {
            Assert.AreEqual(childName, item.Name);
            Assert.AreEqual(false, item.CanExpand);
            Assert.AreEqual(0, item.Children.Count);
        }

        private void isFolder(MenuItemManager.FolderItem item, int count)
        {
            Assert.AreEqual(item.CanExpand, true);
            Assert.AreEqual(item.Children.Count, count);
        }

        private void isFolder(MenuItemManager.FolderItem item, int count, string name)
        {
            isFolder(item, count);
            Assert.AreEqual(name, item.Name);
        }
    }
}

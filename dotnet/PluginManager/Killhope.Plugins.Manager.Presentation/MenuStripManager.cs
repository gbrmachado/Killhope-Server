using Killhope.Plugins.Manager.Domain;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    class MenuStripManager
    {
        private int defaultFirstSize = 41;
        private int defaultSecondSize = 20;
        
        private readonly MenuStrip strip;
        public MenuStripManager(MenuStrip Strip)
        {
            this.strip = Strip;
        }

        private ToolStripMenuItem createNewMenuItem(MenuItemManager.FolderItem item)
        {
            ToolStripMenuItem itemToAdd = new ToolStripMenuItem();
            //itemToAdd.Name = "testToolStripMenuItem";
            itemToAdd.Size = new System.Drawing.Size(defaultFirstSize, defaultSecondSize);
            itemToAdd.Text = item.Name;
            return itemToAdd;
        }

        private ToolStripMenuItem addRootFolder(MenuItemManager.FolderItem item)
        {
            return createNewMenuItem(item);
        }

        private void addItem(MenuItemManager.FolderItem item, ToolStripMenuItem addTo)
        {
            var added = new ToolStripMenuItem(item.Name);
            added.CheckOnClick = true;
            added.Click += (o, ea) => { item.Execute(); };
            addTo.DropDownItems.Add(added);
        }

        /// <summary>
        /// Adds a new item to the existing item and returns the new item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="addTo"></param>
        /// <returns></returns>
        private ToolStripMenuItem addFolder(MenuItemManager.FolderItem item, ToolStripMenuItem addTo)
        {
            var toAdd = createNewMenuItem(item);
            addTo.DropDownItems.Add(toAdd);
            return toAdd;

        }

        public void Merge(MenuItemManager manager)
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            foreach(var item in manager.Items) {
                var folder = addRootFolder(item);
                items.Add(folder);
                Merge(item.Children, folder);
            }

            this.strip.Items.AddRange(items.ToArray());
        }

        /// <summary>
        /// Recursive merge of the tree.
        /// </summary>
        /// <param name="collection"></param>
        private void Merge(IEnumerable<MenuItemManager.FolderItem> collection, ToolStripMenuItem addTo)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");


            foreach (var item in collection)
            {
                if (item.CanExpand)
                {
                    var newFolder = addFolder(item, addTo);
                    //recursive call.
                    Merge(item.Children, newFolder);
                }
                else
                    addItem(item, addTo);
            }

        }
    }


}

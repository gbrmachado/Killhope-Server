using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// Performs transformation from a collection of MenuItems into a tree if Menu Items.
    /// </summary>
    /// <remarks>Handles edge cases of tree creation (for example, adding an item named the same as a folder) and multiple items with the same name.</remarks>
    public class MenuItemManager
    {

        private FolderItemCollection collection;

        public MenuItemManager()
        {
            collection = new FolderItemCollection();
        }

        public void AddMenuItem(NamedMenuItem item)
        {
            collection.Add(item);
        }

        public IEnumerable<FolderItem> Items
        {
            get
            {
                return collection.Items;
            }
        }

        private class FolderItemCollection
        {
            private List<FolderItem> list;

            public FolderItemCollection()
            {
                list = new List<FolderItem>();
            }

            public IEnumerable<FolderItem> Items
            {
                get
                {
                    return list;
                }
            }


            public void Add(NamedMenuItem item)
            {
                Add(0, list, item);
            }

            private static void Add(int treeDepth, List<FolderItem> items, NamedMenuItem toAdd)
            {
                var currentTreeItem = toAdd.Location.Folders.Skip(treeDepth).FirstOrDefault();

                var contained = (from a in items where a.Name == currentTreeItem select a).FirstOrDefault();

                //If there is no contained item, add the current tree as far as it goes, then add the item to the end of the list.
                if (contained == null || currentTreeItem == null) 
                    RecursivePerformAdd(treeDepth, items, toAdd);
                else
                    Add(treeDepth + 1, contained.Children, toAdd);

            }

            private static void RecursivePerformAdd(int treeDepth, List<FolderItem> items, NamedMenuItem toAdd)
            {
                IEnumerable<string> tree = toAdd.Location.Folders.Skip(treeDepth);

                if (tree.Any())
                {
                    var item = new MidFolderItem(tree.First());
                    items.Add(item);
                    RecursivePerformAdd(treeDepth + 1, item.Children, toAdd);
                }
                else
                {
                    items.Add(new EndFolderItem(toAdd));
                }

            }

        }

        public abstract class FolderItem
        {
            protected FolderItem() { }

            public abstract string Name { get; }

            public bool CanExpand { get { return Children.Any(); } }

            public abstract List<FolderItem> Children { get; set; }

            public abstract void Execute();
        }

        [DebuggerDisplay("Item: {Name}")]
        private class EndFolderItem : FolderItem
        {
            private readonly NamedMenuItem Item;
            public EndFolderItem(NamedMenuItem item)
            {
                this.Item = item;
            }

            public override string Name
            {
                get { return Item.PluginName; }
            }

            public override List<FolderItem> Children
            {
                get { return new List<FolderItem>(); }
                //TODO: A set should throw an exception
                set { throw new NotSupportedException(); }
            }


            public override void Execute()
            {
                Item.Action.Invoke();
            }
        }

        [DebuggerDisplay("Folder: {Name}, items: {Children.Count}")]
        private class MidFolderItem : FolderItem
        {
            private readonly string name;
            public MidFolderItem(string name)
            {
                this.name = name;
                this.Children = new List<FolderItem>();
            }

            public override string Name { get { return this.name; } }


            public override List<FolderItem> Children { get; set; }

            public override void Execute() { }
        }
    }
}




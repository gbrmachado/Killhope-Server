using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// Immutable menu item representation.
    /// </summary>
    public class ItemLocation
    {
        private static char Folder_Seperator = '/';

        private static IEnumerable<char> illegalCharacters = new List<char> { Folder_Seperator };
        private IEnumerable<string> folders;

        private string itemName;

        public ItemLocation(string topLevelFolder, string name)
        {
            if (containsIllegalCharacters(topLevelFolder))
                throw new ArgumentException("folder contains invalid characters.");

            this.ItemName = name;
            this.folders = new List<string> { topLevelFolder };
        }

        public static IEnumerable<ItemLocation> GetDistinctFolders(IEnumerable<ItemLocation> locations)
        {
            //TODO: Tests


            List<ItemLocation> ret = new List<ItemLocation>();

            foreach(var loc in locations)
            {
                List<string> folders = new List<string>();
                foreach(string folder in loc.Folders)
                {
                    folders.Add(folder);
                    ItemLocation l = ItemLocation.FromFolderTree(folders);
                    if (!ret.Contains(l))
                        ret.Add(l);
                }

            }

            return ret;

        }

        internal ItemLocation Append(ItemLocation location)
        {
            throw new NotImplementedException();
        }




        //Private constructor, assume that name is already validated.
        private ItemLocation(IEnumerable<string> folders, string itemName)
        {
            this.folders = folders;
            this.ItemName = itemName;
        }

        public string ItemName
        {
            get { return itemName; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("name");

                if (containsIllegalCharacters(value))
                    throw new ArgumentException("name contains invalid characters.");

                itemName = value;
            }
        }



        /// <summary>
        /// A list of the folder tree of the item (starting at the outer-most folder, and working inwards).
        /// </summary>
        /// <remarks>May not return an empty collection or null.</remarks>
        public IEnumerable<string> Folders { get { return folders; } }

        public static ItemLocation Root { get { return new ItemLocation("", ""); } }

        public ItemLocation AddFolder(string folderName)
        {
            if (folderName == null)
                throw new ArgumentNullException("folderName");

            if (containsIllegalCharacters(folderName))
                throw new ArgumentException("folder contains invalid characters.");

            return new ItemLocation(this.folders.Concat(new[] { folderName }), this.ItemName);
        }


        private bool containsIllegalCharacters(string path)
        {
            foreach (char c in illegalCharacters)
                if (path.Contains(c))
                    return true;

            return false;
        }

        public override string ToString()
        {
            string folders = String.Join(Folder_Seperator.ToString(), this.folders);
            return folders + Folder_Seperator + ItemName;
        }




        public IEnumerable<string> GetFolderSimilarities(ItemLocation two)
        {
            if (two == null)
                throw new ArgumentNullException("two");

            List<string> ret = new List<string>();

            foreach (var x in this.Folders.Zip(two.Folders,(a,b) => new Tuple<string,string>(a,b)))
            {
                if (!x.Item1.Equals(x.Item2))
                    break;
                ret.Add(x.Item1);
            }

            return ret;

        }


        internal static ItemLocation FromFolderTree(IEnumerable<string> currentRelativePath)
        {
            if(currentRelativePath == null || !currentRelativePath.Any())
                return new ItemLocation("","");


            ItemLocation initial = new ItemLocation(currentRelativePath.First(), "");
            while (currentRelativePath.Any())
            {
                initial.AddFolder(currentRelativePath.First());
                currentRelativePath = currentRelativePath.Skip(1);
            }

            return initial;
        }

        public ItemLocation SetName(string itemName)
        {
            return new ItemLocation(this.folders, itemName);
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ItemLocation test = obj as ItemLocation;

            if (test == null)
                return false;

            if (this.ItemName != test.ItemName)
                return false;

            return this.folders.SequenceEqual(test.Folders);
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;

            foreach (string s in folders.Concat(new[] { itemName }))
                result = result * prime + s.GetHashCode();

            return result;
        }

        public string ToPath()
        {
            return ToPath(Path.DirectorySeparatorChar.ToString());
        }

        public string ToPath(string directorySeperator)
        {
            string ret = directorySeperator;
            foreach(var s in folders)
                ret += s + directorySeperator;

            ret += this.ItemName;
            return ret;
        }

        public static ItemLocation FromRelativeFilePath(string file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (!file.StartsWith(Path.DirectorySeparatorChar.ToString()))
                throw new ArgumentException($"path: {file} does not start with {Path.DirectorySeparatorChar}");

            string fileName = Path.GetFileName(file);
            string folderPath = Path.GetDirectoryName(file);
            IEnumerable<string> paths = folderPath.Split(Path.DirectorySeparatorChar);

            return ItemLocation.FromFolderTree(paths).SetName(fileName);
        }
    }

}

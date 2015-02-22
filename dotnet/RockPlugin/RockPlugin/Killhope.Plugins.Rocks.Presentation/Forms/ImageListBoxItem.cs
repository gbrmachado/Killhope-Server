using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Presentation
{
    public class ImageListBoxItem
    {
        public static readonly string DisplayMember = "Name";
        public static readonly string ValueMember = "FilePath";

        public string Name { get; private set; }
        public string FilePath { get; private set; }

        public ImageListBoxItem(string name, string path)
        {
            this.Name = name;
            this.FilePath = path;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Manager.Domain
{ 
    /// <summary>
    /// Encapsualtes a file location and its contents.
    /// </summary>
    public abstract class File
    {
        public abstract byte[] GetContent();
        
        public ItemLocation Location { get; private set; }

        public File(ItemLocation location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            this.Location = location;
        }
    }
}

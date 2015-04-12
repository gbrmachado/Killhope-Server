using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// A menu item with an associated name.
    /// </summary>
    public class NamedMenuItem : MenuItem
    {
        //TODO: Offer a constructor which would allow the overriding of the name, pull the name down to the base class.

        public string PluginName { get { return this.Location.ItemName; } }

        public NamedMenuItem(ItemLocation location, Action action) : base(location, action) 
        {

        }
    }
}

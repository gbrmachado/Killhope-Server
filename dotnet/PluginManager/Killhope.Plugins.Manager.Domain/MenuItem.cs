using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    public class MenuItem
    {
       

        public MenuItem(ItemLocation location, Action action)
        {
            this.Location = location;
            this.Action = action;
        }

        public ItemLocation Location { private set; get; }
        public Action Action { private set; get; }
    }
}

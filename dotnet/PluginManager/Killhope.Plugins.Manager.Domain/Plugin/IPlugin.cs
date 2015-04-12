using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    public interface IPlugin
    {
        /// <summary>
        /// The unique identifier for the plugin.
        /// </summary>
        /// <remarks>Allows conflict resolution via version number when two of the same plugin ae detected.</remarks>
        Guid UID { get; }

        int MajorVersion { get; }
        int MinorVersion { get; }

        /// <summary>
        /// The displayed name of the plugin.
        /// </summary>
        string Name { get; }
        void Execute();

        /// <summary>
        /// A list of items to add to the menu with associated actions.
        /// </summary>
        IEnumerable<Tuple<Action, ItemLocation>> MenuItems { get; }
       
    }
}

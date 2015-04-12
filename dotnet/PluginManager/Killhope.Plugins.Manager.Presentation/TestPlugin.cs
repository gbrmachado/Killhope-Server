using Killhope.Plugins.Manager.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    [Export(typeof(IPlugin))]
    class TestPlugin : IPlugin
    {
        public string Name
        {
            get { throw new NotSupportedException(); }
        }

        public void Execute()
        {
            throw new NotSupportedException();
        }

        private void Message()
        {
            MessageBox.Show("hello");
        }

        public IEnumerable<Tuple<Action, ItemLocation>> MenuItems
        {
            get 
            {
                var loc = new ItemLocation("Tools","PluginTest");
                List<Tuple<Action, ItemLocation>> ret = new List<Tuple<Action, ItemLocation>>();
                ret.Add(new Tuple<Action, ItemLocation>(Message, loc));
                ret.Add(new Tuple<Action, ItemLocation>(Message, loc.AddFolder("Temp")));
                ret.Add(new Tuple<Action, ItemLocation>(Message, loc.AddFolder("FLDR")));
                ret.Add(new Tuple<Action, ItemLocation>(Message, new ItemLocation("Tools", "P2")));
                ret.Add(new Tuple<Action, ItemLocation>(Message, new ItemLocation("File", "P2")));
                return ret;
            }
        }

        public Guid UID
        {
            get { throw new NotSupportedException(); }
        }

        public int MajorVersion
        {
            get { throw new NotSupportedException(); }
        }

        public int MinorVersion
        {
            get { throw new NotSupportedException(); }
        }
    }
}

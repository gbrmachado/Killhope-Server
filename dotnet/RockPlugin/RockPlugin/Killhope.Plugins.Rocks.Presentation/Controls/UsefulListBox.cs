using Killhope.Plugins.Rocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Presentation.Controls
{
    class UsefulListBox : ReorderableListBox<Content>
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.lbx_Data.DisplayMember = "Title";
        }
    }
}

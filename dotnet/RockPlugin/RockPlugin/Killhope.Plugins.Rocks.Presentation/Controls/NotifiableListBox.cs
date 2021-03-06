﻿using System;
using System.Windows.Forms;

//Code from: http://stackoverflow.com/q/655956

namespace Killhope.Plugins.Rocks.Presentation.Controls
{
    /// <summary>
    /// A listbox with ItemsChanged event handle
    /// </summary>
    public class NotifiableListBox : ListBox
    {
        private const int LB_ADDSTRING = 0x180;
        private const int LB_INSERTSTRING = 0x181;
        private const int LB_DELETESTRING = 0x182;
        private const int LB_RESETCONTENT = 0x184;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == LB_ADDSTRING ||
                m.Msg == LB_INSERTSTRING ||
                m.Msg == LB_DELETESTRING ||
                m.Msg == LB_RESETCONTENT)
            {
                ItemsChanged(this, EventArgs.Empty);
            }
            base.WndProc(ref m);
        }

        public event EventHandler ItemsChanged = delegate { };
    }
}


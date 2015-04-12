using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    public partial class ReleasePicker : Form
    {
        private readonly IEnumerable<int> available;

        public int Result { get; set; }

        [Obsolete("Designer only",true)]
        public ReleasePicker()
        {
            InitializeComponent();
        }

        public ReleasePicker(IEnumerable<int> releases)
        {
            InitializeComponent();
            this.available = releases;
        }

        private void ReleasePicker_Load(object sender, EventArgs e)
        {
            if (this.available == null)
                throw new ArgumentNullException("no releases to choose from.");

            cbo_releaseNumber.Items.AddRange(available.Cast<object>().ToArray());

            SetVariables();
        }

        private void cbo_releaseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVariables();
        }

        private void SetVariables()
        {
            btn_OK.Enabled = cbo_releaseNumber.SelectedIndex != -1;
            if (cbo_releaseNumber.SelectedIndex != -1)
                this.Result = (int)cbo_releaseNumber.SelectedItem;
        }
    }
}

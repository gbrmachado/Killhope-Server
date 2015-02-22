using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Rocks.Presentation
{
    public partial class JSONSelection : Form
    {
        public JSONSelection()
        {
            InitializeComponent();

#if DEBUG 
            //If we are in debug mode, allow any json rather than just rocks.json.
            filePickerUser1.Filter = "JSON|*.json";
#endif
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            var form = new frm_RockList(new ExistingJSONFileHandler(this.Path));
            form.ShowDialog();
        }

        public string Path { get { return this.filePickerUser1.FilePath; } }
    }
}

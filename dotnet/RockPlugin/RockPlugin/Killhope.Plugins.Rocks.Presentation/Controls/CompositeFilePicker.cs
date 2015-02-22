using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Rocks.Presentation
{
    public partial class CompositeFilePicker : UserControl
    {
        public class FileChangedEventArgs : EventArgs
        {

        }

        public delegate void FileChangedEvent(object sender, FileChangedEventArgs eventArgs);

        public event FileChangedEvent FileChanged;

        private string filter;

        public CompositeFilePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The filter to use when browsing for files.
        /// </summary>
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                if (String.IsNullOrEmpty(value))
                    throw new InvalidOperationException("No file types have been specified for this control.");
            }
        }

        public string FilePath {
            get { return this.txt_FileSelected.Text; }
            set { 
                this.txt_FileSelected.Text = value;
                OnFileChanged(new FileChangedEventArgs());
                CanClear = !String.IsNullOrEmpty(value);
            } 
        }

        protected virtual void OnFileChanged(FileChangedEventArgs e) {
            if(FileChanged != null)
                FileChanged(this, e);
        }

        private int clearWidth { get { return btn_Clear.Width; } }

        [DefaultValue(true)]
        public bool ShowClear { 
            get { return btn_Clear.Visible; } 
            set {
                if (value != btn_Clear.Visible)
                {
                    int newX;
                    if (value)
                    {
                        newX = btn_Browse.Location.X - clearWidth;
                        this.txt_FileSelected.Width -= clearWidth;
                    }
                    else
                    {
                        newX = btn_Browse.Location.X + clearWidth;
                        this.txt_FileSelected.Width += clearWidth;
                    }

                    btn_Browse.Location = new Point(newX, btn_Browse.Location.Y);

                }
                btn_Clear.Visible = value; 
            } 
        }

        private bool canClear;
        public bool CanClear {
            get { return canClear; } 
            private set { 
                btn_Clear.Enabled = value && this.Enabled;
                canClear = value;
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            this.FilePath = "";
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Filter))
                throw new InvalidOperationException("No file types have been specified for this control.");

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = this.Filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            //openFileDialog.FileName actually returns the file path.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                FilePath = openFileDialog1.FileName;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            this.btn_Browse.Enabled = this.Enabled;
            btn_Clear.Enabled = CanClear && this.Enabled; 
        }


    }
}

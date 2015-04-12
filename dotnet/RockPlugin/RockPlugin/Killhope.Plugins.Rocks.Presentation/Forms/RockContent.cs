using Killhope.Plugins.Rocks.Domain;
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
    public partial class RockContent : Form, IContentEditorView
    {

        public string Data
        {
            get
            {
                //TODO: From this.richTextBox1 to a string.
                throw new NotImplementedException();

            }
            set
            {
                if (value == null)
                    value = "";
                LoadFromRichText(new RichText(value), this.richTextBox1);
            }
        }

        public string Title
        {
            get { return this.textBox1.Text; }
            set {  this.textBox1.Text = value; }
        }

        public RockContent()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Data = "<b>Hi <u>There,</b> this</u> <b><u>is</u></b> an initial Test.<br/><br/><u>Hello World</u>><br/><b>Hello World</b>><br/><i>Hello World</i>";
                this.Title = "Designer - Generated Content";
                MessageBox.Show("HELLO");
            }

            
        }

        private static void LoadFromRichText(RichText text, RichTextBox textBox)
        {
            textBox.Text = text.Value;

            //TODO: Encapsulate and test using XOR to flip the flag.,
            foreach (var loc in text.UnderlineLocations)
            {
                textBox.Select(loc.Start, loc.Length);
                textBox.SelectionFont =  new Font(textBox.SelectionFont, textBox.SelectionFont.Style ^ FontStyle.Underline);
            }


            foreach (var loc in text.BoldLocations)
            {
                textBox.Select(loc.Start, loc.Length);
                textBox.SelectionFont = new Font(textBox.SelectionFont, textBox.SelectionFont.Style ^ FontStyle.Bold);
            }

            int counter = 0;
            foreach (var loc in text.LinebreakLocations)
            {
                string shouldBeDone = text.Value.Substring(loc.Start + counter, 5);
                //TODO: Why are we doing -1 here?
                textBox.Select(loc.Start + counter, loc.Length);
                //Although a newLine is \r\n, the textbox changes this to \n. We will want to fix this before implementation is finished.
                counter += "\n".Length;
                textBox.SelectedText = Environment.NewLine;
            }

            textBox.Select(0, 0);
        }

        void IContentEditorView.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}

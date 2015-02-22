using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    public partial class SiteSelection : Form
    {
        private readonly List<SiteManifest> manifests;
        private readonly SiteManifestManager manager;
        /// <summary>
        /// Whether we are adding a new Manifest.
        /// </summary>
        private bool isNew;
        private int currentIndex = -1;

        public SiteManifest Result { get; set; }
        public string PasswordResult { get; set; }

        public SiteSelection()
        {
            InitializeComponent();
        }


        public SiteSelection(SiteManifestManager manifests) : this()
        {
            this.manifests = manifests.GetManifests().ToList();
            this.manager = manifests;
        }

        private void SiteSelection_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = manifests;
            RefreshVariables();

            if (manifests.Any())
                this.selectManifest(0);
        }

        private void selectManifest(int index)
        {
            this.comboBox1.SelectedIndex = index;
            SiteManifest selected = this.manifests[this.comboBox1.SelectedIndex];

            this.txt_DisplayName.Text = selected.DisplayName;
            this.txt_Domain.Text = selected.FTPServer;
            this.txt_RootFolder.Text = selected.FTPPath;
            this.cbx_secure.Checked = selected.UseSFTP;
            this.txt_Username.Text = selected.DefaultUserName;
        }

        private void RefreshVariables()
        {
            btn_OK.Enabled = manifests.Any();
            btn_Save.Enabled = manifests.Any();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            this.manifests.Add(new SiteManifest());
            this.isNew = true;
            RefreshVariables();
            this.selectManifest(this.manifests.Count - 1);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            SiteManifest selected = this.manifests[this.comboBox1.SelectedIndex];
            
            this.Result = selected;
            this.DialogResult = DialogResult.OK;
            this.PasswordResult = txt_Password.Text;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

        }
    }
}

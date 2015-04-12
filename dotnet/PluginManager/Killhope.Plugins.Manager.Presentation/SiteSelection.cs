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
        private readonly BindingList<SiteManifest> manifests;
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
            this.manifests = new BindingList<SiteManifest>(manifests.GetManifests().ToList());
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
            this.manifests.Add(this.CreateManifestFromFields());
            this.isNew = true;
            RefreshVariables();
            this.selectManifest(this.manifests.Count - 1);
        }

        private SiteManifest CreateManifestFromFields()
        {
            SiteManifest m = new SiteManifest();
            m.DisplayName = this.txt_DisplayName.Text;
            m.DefaultUserName = this.UserName;
            m.FTPPath = this.RootFolder;
            m.FTPServer = FTPDomain;
            m.UseSFTP = this.IsSFTP;
            return m;
        }

        public string DisplayName { get { return txt_DisplayName.Text; } set { txt_DisplayName.Text = value; } }
        public string UserName { get { return txt_Username.Text; } set { txt_Username.Text = value; } }
        public string RootFolder { get { return txt_RootFolder.Text; } set { txt_RootFolder.Text = value; } }
        public string FTPDomain { get { return txt_Domain.Text; } set { txt_Domain.Text = value; } }
        public string Password { get { return txt_Password.Text; } set { txt_Password.Text = value; } }
        public bool IsSFTP { get { return cbx_secure.Checked; } set { cbx_secure.Checked = value; } }

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

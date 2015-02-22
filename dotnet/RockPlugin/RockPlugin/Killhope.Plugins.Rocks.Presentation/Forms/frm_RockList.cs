using Killhope.Plugins.Rocks.Domain.Application;
using Killhope.Plugins.Rocks.Presentation.Properties;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RockListDS = Killhope.Plugins.Rocks.Domain.RockList;

namespace Killhope.Plugins.Rocks.Presentation
{
    public partial class frm_RockList : Form, IRockListView
    {
        //Used to determine whether changes have occurred, so we can retet the save button.
        private bool hasChanged = true;


        private MessageServiceImpl messageService;

        private readonly RockListController controller;
        private bool canEdit;

        [Obsolete("Only used for the designer.", true)]
        public frm_RockList()
        {
            InitializeComponent();
        }

        public frm_RockList(IJSONModificationService rockList)
        {
            InitializeComponent();
            this.messageService = new MessageServiceImpl();
            this.controller = new RockListController(rockList, this.messageService, this, new ContentEditorViewModel(new RockContent()));
            controller.Initialise();
        }

        private Domain.Content SelectedContent
        {
            get { return (Domain.Content)lbx_Content.SelectedValue; }
        }

        public List<Domain.Content> Content
        {
            get { return lbx_Content.Items.ToList(); }
            set { lbx_Content.Items = value; }
        }

        public string Formula { get { return txt_Formula.Text; } set { txt_Formula.Text = value; } }

        public List<int> GalleryImages
        {
            get
            {
                var checed = lbx_Images.CheckedIndices;
                return checed.Cast<int>().ToList();
            }
            set
            {
                //uncheck all
                for (int i = 0; i < lbx_Images.Items.Count; i++)
                    lbx_Images.SetItemChecked(i, false);

                if (value == null)
                    return;

                foreach (int i in value)
                    lbx_Images.SetItemChecked(i, true);
            }
        }

        public List<string> Images
        {
            get
            {
                return (from a in lbx_Images.SelectedItems.Cast<ImageListBoxItem>() select a.FilePath).ToList();
            }
            set
            {
                lbx_Images.Items.Clear();

                if (value == null)
                    return;

                foreach (var item in value)
                    AddImage(item);
            }
        }

        public string Title { get { return txt_Title.Text; } set { txt_Title.Text = value; } }

        public string UniqueId { get { return txt_UID.Text; } set { txt_UID.Text = value; } }

        public string VideoPath { get { return fp_VideoPicker.FilePath; } set { fp_VideoPicker.FilePath = value; } }

        public bool CanSave
        {
            get { return hasChanged; }
        }

        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                txt_Formula.Enabled = value;
                txt_Title.Enabled = value;
                txt_UID.Enabled = value;
                fp_VideoPicker.Enabled = value;
                btn_AddImage.Enabled = value;
                btn_RemoveImage.Enabled = value && canRemoveImage;
                lbx_Images.Enabled = value;
                lbx_Content.Enabled = value;
                btn_AddContent.Enabled = value;
            }
        }

        private bool canRemoveImage { get { return this.lbx_Images.SelectedIndex != -1; } }

        public void LoadRock(Domain.Rock selected)
        {
            //TODO: hasChanged = false;

            this.CanEdit = selected != null;
            this.btn_SaveRock.Enabled = controller.CanSave();
            this.btn_NewRock.Enabled = controller.CanAdd();
            this.btn_DeleteRock.Enabled = controller.CanDelete();
            this.btn_PreviousRock.Enabled = controller.CanGoPrevious();
            this.btn_NextRock.Enabled = controller.CanGoNext();
            this.btn_SaveRock.Enabled = CanSave && controller.CanSave();
            btn_RemoveImage.Enabled = canRemoveImage;
        }

        private void btn_AddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = Settings.Default.ImagePathFilter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            //openFileDialog.FileName actually returns the file path.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                AddImage(openFileDialog1.FileName);
        }

        private void AddImage(string path)
        {
            string name = Path.GetFileName(path);

            this.lbx_Images.Items.Add(new ImageListBoxItem(name, path), false);
            //select the item that we just added.
            lbx_Images.SelectedIndex = lbx_Images.Items.Count - 1;
            btn_RemoveImage.Enabled = true;
        }

        private void btn_RemoveImage_Click(object sender, EventArgs e)
        {
            int index = this.lbx_Images.SelectedIndex;
            if (index == -1)
            {
                //Logical error, but we do not want to stop the execution on a release build.
                btn_RemoveImage.Enabled = false;
                if (Debugger.IsAttached)
                    Debugger.Break();
                return;
            }
            this.lbx_Images.Items.RemoveAt(index);
            btn_RemoveImage.Enabled = this.lbx_Images.SelectedIndex != -1;

        }

        private void lbx_Images_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_RemoveImage.Enabled = this.lbx_Images.SelectedIndex != -1;
        }

        private void RockList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.controller.TryClose();
        }

        private void btn_NewRock_Click(object sender, EventArgs e)
        {
            controller.Add();
        }

        private void btn_DeleteRock_Click(object sender, EventArgs e)
        {
            controller.Delete();
        }

        private void btn_SaveRock_Click(object sender, EventArgs e)
        {
            controller.Save();
        }

        private void btn_NextRock_Click(object sender, EventArgs e)
        {
            this.controller.GoNext();
        }

        private void btn_PreviousRock_Click(object sender, EventArgs e)
        {
            this.controller.GoPrevious();
        }

        private void btn_AddContent_Click(object sender, EventArgs e)
        {
            string title = Interaction.InputBox("Please enter the title", "Enter Title");
            if (string.IsNullOrEmpty(title))
                return;

            lbx_Content.Add(new Domain.Content() { Title = title, Data = "" });
        }

        private void btn_EditContent_Click(object sender, EventArgs e)
        {
            this.controller.EditContent(this.SelectedContent);
        }
    }
}

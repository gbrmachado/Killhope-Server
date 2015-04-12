using Killhope.Plugins.Rocks.Presentation.Properties;
namespace Killhope.Plugins.Rocks.Presentation
{
    partial class frm_RockList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_NextRock = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_PreviousRock = new System.Windows.Forms.Button();
            this.btn_DeleteRock = new System.Windows.Forms.Button();
            this.btn_NewRock = new System.Windows.Forms.Button();
            this.btn_SaveRock = new System.Windows.Forms.Button();
            this.lbl_UniqueID = new System.Windows.Forms.Label();
            this.txt_UID = new System.Windows.Forms.TextBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_VideoPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbx_Images = new System.Windows.Forms.CheckedListBox();
            this.btn_AddImage = new System.Windows.Forms.Button();
            this.btn_RemoveImage = new System.Windows.Forms.Button();
            this.txt_Formula = new System.Windows.Forms.TextBox();
            this.lbl_Formula = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_AddContent = new System.Windows.Forms.Button();
            this.lbx_Content = new Killhope.Plugins.Rocks.Presentation.Controls.UsefulListBox();
            this.fp_VideoPicker = new Killhope.Plugins.Rocks.Presentation.CompositeFilePicker();
            this.btn_EditContent = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_NextRock);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_PreviousRock);
            this.panel1.Controls.Add(this.btn_DeleteRock);
            this.panel1.Controls.Add(this.btn_NewRock);
            this.panel1.Controls.Add(this.btn_SaveRock);
            this.panel1.Location = new System.Drawing.Point(18, 342);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 57);
            this.panel1.TabIndex = 1;
            // 
            // btn_NextRock
            // 
            this.btn_NextRock.Location = new System.Drawing.Point(120, 15);
            this.btn_NextRock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_NextRock.Name = "btn_NextRock";
            this.btn_NextRock.Size = new System.Drawing.Size(45, 35);
            this.btn_NextRock.TabIndex = 6;
            this.btn_NextRock.Text = ">>";
            this.btn_NextRock.UseVisualStyleBackColor = true;
            this.btn_NextRock.Click += new System.EventHandler(this.btn_NextRock_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 26);
            this.textBox1.TabIndex = 5;
            // 
            // btn_PreviousRock
            // 
            this.btn_PreviousRock.Location = new System.Drawing.Point(3, 15);
            this.btn_PreviousRock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_PreviousRock.Name = "btn_PreviousRock";
            this.btn_PreviousRock.Size = new System.Drawing.Size(45, 35);
            this.btn_PreviousRock.TabIndex = 4;
            this.btn_PreviousRock.Text = "<<";
            this.btn_PreviousRock.UseVisualStyleBackColor = true;
            this.btn_PreviousRock.Click += new System.EventHandler(this.btn_PreviousRock_Click);
            // 
            // btn_DeleteRock
            // 
            this.btn_DeleteRock.Location = new System.Drawing.Point(516, 14);
            this.btn_DeleteRock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_DeleteRock.Name = "btn_DeleteRock";
            this.btn_DeleteRock.Size = new System.Drawing.Size(112, 35);
            this.btn_DeleteRock.TabIndex = 3;
            this.btn_DeleteRock.Text = "Delete";
            this.btn_DeleteRock.UseVisualStyleBackColor = true;
            this.btn_DeleteRock.Click += new System.EventHandler(this.btn_DeleteRock_Click);
            // 
            // btn_NewRock
            // 
            this.btn_NewRock.Location = new System.Drawing.Point(394, 14);
            this.btn_NewRock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_NewRock.Name = "btn_NewRock";
            this.btn_NewRock.Size = new System.Drawing.Size(112, 35);
            this.btn_NewRock.TabIndex = 2;
            this.btn_NewRock.Text = "New";
            this.btn_NewRock.UseVisualStyleBackColor = true;
            this.btn_NewRock.Click += new System.EventHandler(this.btn_NewRock_Click);
            // 
            // btn_SaveRock
            // 
            this.btn_SaveRock.Location = new System.Drawing.Point(638, 14);
            this.btn_SaveRock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SaveRock.Name = "btn_SaveRock";
            this.btn_SaveRock.Size = new System.Drawing.Size(112, 35);
            this.btn_SaveRock.TabIndex = 1;
            this.btn_SaveRock.Text = "Save";
            this.btn_SaveRock.UseVisualStyleBackColor = true;
            this.btn_SaveRock.Click += new System.EventHandler(this.btn_SaveRock_Click);
            // 
            // lbl_UniqueID
            // 
            this.lbl_UniqueID.AutoSize = true;
            this.lbl_UniqueID.Location = new System.Drawing.Point(16, 23);
            this.lbl_UniqueID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_UniqueID.Name = "lbl_UniqueID";
            this.lbl_UniqueID.Size = new System.Drawing.Size(81, 20);
            this.lbl_UniqueID.TabIndex = 2;
            this.lbl_UniqueID.Text = "Unique ID";
            // 
            // txt_UID
            // 
            this.txt_UID.Location = new System.Drawing.Point(108, 18);
            this.txt_UID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_UID.Name = "txt_UID";
            this.txt_UID.Size = new System.Drawing.Size(148, 26);
            this.txt_UID.TabIndex = 3;
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(333, 18);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(148, 26);
            this.txt_Title.TabIndex = 5;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Location = new System.Drawing.Point(284, 23);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(38, 20);
            this.lbl_Title.TabIndex = 4;
            this.lbl_Title.Text = "Title";
            // 
            // lbl_VideoPath
            // 
            this.lbl_VideoPath.AutoSize = true;
            this.lbl_VideoPath.Location = new System.Drawing.Point(15, 71);
            this.lbl_VideoPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_VideoPath.Name = "lbl_VideoPath";
            this.lbl_VideoPath.Size = new System.Drawing.Size(87, 20);
            this.lbl_VideoPath.TabIndex = 7;
            this.lbl_VideoPath.Text = "Video Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(558, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Images";
            // 
            // lbx_Images
            // 
            this.lbx_Images.FormattingEnabled = true;
            this.lbx_Images.Location = new System.Drawing.Point(562, 58);
            this.lbx_Images.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbx_Images.Name = "lbx_Images";
            this.lbx_Images.Size = new System.Drawing.Size(228, 130);
            this.lbx_Images.TabIndex = 10;
            this.lbx_Images.SelectedIndexChanged += new System.EventHandler(this.lbx_Images_SelectedIndexChanged);
            // 
            // btn_AddImage
            // 
            this.btn_AddImage.Location = new System.Drawing.Point(562, 212);
            this.btn_AddImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_AddImage.Name = "btn_AddImage";
            this.btn_AddImage.Size = new System.Drawing.Size(112, 35);
            this.btn_AddImage.TabIndex = 11;
            this.btn_AddImage.Text = "Add";
            this.btn_AddImage.UseVisualStyleBackColor = true;
            this.btn_AddImage.Click += new System.EventHandler(this.btn_AddImage_Click);
            // 
            // btn_RemoveImage
            // 
            this.btn_RemoveImage.Enabled = false;
            this.btn_RemoveImage.Location = new System.Drawing.Point(680, 212);
            this.btn_RemoveImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_RemoveImage.Name = "btn_RemoveImage";
            this.btn_RemoveImage.Size = new System.Drawing.Size(112, 35);
            this.btn_RemoveImage.TabIndex = 12;
            this.btn_RemoveImage.Text = "Remove";
            this.btn_RemoveImage.UseVisualStyleBackColor = true;
            this.btn_RemoveImage.Click += new System.EventHandler(this.btn_RemoveImage_Click);
            // 
            // txt_Formula
            // 
            this.txt_Formula.Location = new System.Drawing.Point(110, 109);
            this.txt_Formula.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Formula.Name = "txt_Formula";
            this.txt_Formula.Size = new System.Drawing.Size(148, 26);
            this.txt_Formula.TabIndex = 15;
            // 
            // lbl_Formula
            // 
            this.lbl_Formula.AutoSize = true;
            this.lbl_Formula.Location = new System.Drawing.Point(18, 114);
            this.lbl_Formula.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Formula.Name = "lbl_Formula";
            this.lbl_Formula.Size = new System.Drawing.Size(67, 20);
            this.lbl_Formula.TabIndex = 14;
            this.lbl_Formula.Text = "Formula";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Content";
            // 
            // btn_AddContent
            // 
            this.btn_AddContent.Location = new System.Drawing.Point(110, 297);
            this.btn_AddContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_AddContent.Name = "btn_AddContent";
            this.btn_AddContent.Size = new System.Drawing.Size(106, 35);
            this.btn_AddContent.TabIndex = 18;
            this.btn_AddContent.Text = "Add";
            this.btn_AddContent.UseVisualStyleBackColor = true;
            this.btn_AddContent.Click += new System.EventHandler(this.btn_AddContent_Click);
            // 
            // lbx_Content
            // 
            this.lbx_Content.Location = new System.Drawing.Point(99, 149);
            this.lbx_Content.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.lbx_Content.Name = "lbx_Content";
            this.lbx_Content.SelectedIndex = -1;
            this.lbx_Content.Size = new System.Drawing.Size(225, 151);
            this.lbx_Content.TabIndex = 17;
            // 
            // fp_VideoPicker
            // 
            this.fp_VideoPicker.FilePath = "";
            this.fp_VideoPicker.Filter = global::Killhope.Plugins.Rocks.Presentation.Properties.Settings.Default.VideoPathFilter;
            this.fp_VideoPicker.Location = new System.Drawing.Point(108, 58);
            this.fp_VideoPicker.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.fp_VideoPicker.Name = "fp_VideoPicker";
            this.fp_VideoPicker.Size = new System.Drawing.Size(375, 43);
            this.fp_VideoPicker.TabIndex = 6;
            // 
            // btn_EditContent
            // 
            this.btn_EditContent.Location = new System.Drawing.Point(224, 297);
            this.btn_EditContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_EditContent.Name = "btn_EditContent";
            this.btn_EditContent.Size = new System.Drawing.Size(106, 35);
            this.btn_EditContent.TabIndex = 19;
            this.btn_EditContent.Text = "Edit";
            this.btn_EditContent.UseVisualStyleBackColor = true;
            this.btn_EditContent.Click += new System.EventHandler(this.btn_EditContent_Click);
            // 
            // frm_RockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 417);
            this.Controls.Add(this.btn_EditContent);
            this.Controls.Add(this.btn_AddContent);
            this.Controls.Add(this.lbx_Content);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Formula);
            this.Controls.Add(this.lbl_Formula);
            this.Controls.Add(this.btn_RemoveImage);
            this.Controls.Add(this.btn_AddImage);
            this.Controls.Add(this.lbx_Images);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_VideoPath);
            this.Controls.Add(this.fp_VideoPicker);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.txt_UID);
            this.Controls.Add(this.lbl_UniqueID);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_RockList";
            this.Text = "RockList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RockList_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_DeleteRock;
        private System.Windows.Forms.Button btn_NewRock;
        private System.Windows.Forms.Button btn_SaveRock;
        private System.Windows.Forms.Label lbl_UniqueID;
        private System.Windows.Forms.TextBox txt_UID;
        private System.Windows.Forms.Button btn_NextRock;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_PreviousRock;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Label lbl_Title;
        private CompositeFilePicker fp_VideoPicker;
        private System.Windows.Forms.Label lbl_VideoPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox lbx_Images;
        private System.Windows.Forms.Button btn_AddImage;
        private System.Windows.Forms.Button btn_RemoveImage;
        private System.Windows.Forms.TextBox txt_Formula;
        private System.Windows.Forms.Label lbl_Formula;
        private System.Windows.Forms.Label label1;
        private Controls.UsefulListBox lbx_Content;
        private System.Windows.Forms.Button btn_AddContent;
        private System.Windows.Forms.Button btn_EditContent;
    }
}
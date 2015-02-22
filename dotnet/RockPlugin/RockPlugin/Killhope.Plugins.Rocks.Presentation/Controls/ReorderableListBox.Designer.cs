namespace Killhope.Plugins.Rocks.Presentation.Controls
{
    partial class ReorderableListBox<T>
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.lbx_Data = new Killhope.Plugins.Rocks.Presentation.Controls.NotifiableListBox();
            this.SuspendLayout();
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(84, 62);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(50, 23);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Down.Location = new System.Drawing.Point(84, 32);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(50, 23);
            this.btn_Down.TabIndex = 3;
            this.btn_Down.Text = "▼";
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Up.Location = new System.Drawing.Point(84, 3);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(50, 23);
            this.btn_Up.TabIndex = 4;
            this.btn_Up.Text = "▲";
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // lbx_Data
            // 
            this.lbx_Data.FormattingEnabled = true;
            this.lbx_Data.Location = new System.Drawing.Point(8, 4);
            this.lbx_Data.Name = "lbx_Data";
            this.lbx_Data.Size = new System.Drawing.Size(70, 82);
            this.lbx_Data.TabIndex = 5;
            this.lbx_Data.ItemsChanged += new System.EventHandler(this.notifiableListBox1_ItemsChanged);
            this.lbx_Data.SelectedIndexChanged += new System.EventHandler(this.lbx_Data_SelectedIndexChanged);
            // 
            // ReorderableListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbx_Data);
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.btn_Down);
            this.Controls.Add(this.btn_Delete);
            this.Name = "ReorderableListBox";
            this.Size = new System.Drawing.Size(150, 88);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btn_Delete;
        protected System.Windows.Forms.Button btn_Down;
        protected System.Windows.Forms.Button btn_Up;
        protected NotifiableListBox lbx_Data;

    }
}

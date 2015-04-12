namespace Killhope.Plugins.Rocks.Presentation
{
    partial class JSONSelection
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
            this.filePickerUser1 = new Killhope.Plugins.Rocks.Presentation.CompositeFilePicker();
            this.btn_open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filePickerUser1
            // 
            this.filePickerUser1.FilePath = "";
            this.filePickerUser1.Filter = "Rock Properties|rocks.json";
            this.filePickerUser1.Location = new System.Drawing.Point(12, 12);
            this.filePickerUser1.Name = "filePickerUser1";
            this.filePickerUser1.ShowClear = false;
            this.filePickerUser1.Size = new System.Drawing.Size(210, 28);
            this.filePickerUser1.TabIndex = 0;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(219, 13);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 1;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // JSONSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 50);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.filePickerUser1);
            this.Name = "JSONSelection";
            this.Text = "JSONSelection";
            this.ResumeLayout(false);

        }

        #endregion

        private CompositeFilePicker filePickerUser1;
        private System.Windows.Forms.Button btn_open;
    }
}
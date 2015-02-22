namespace Killhope.Plugins.Maps.Presentation
{
    partial class Form1
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
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnl_Room = new System.Windows.Forms.Panel();
            this.btn_DeleteRoom = new System.Windows.Forms.Button();
            this.lbx_Rooms = new System.Windows.Forms.ListBox();
            this.btn_AddRoom = new System.Windows.Forms.Button();
            this.lbl_ListRooms = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_deleteExhibition = new System.Windows.Forms.Button();
            this.btn_AddExhibition = new System.Windows.Forms.Button();
            this.lbx_exhibitions = new System.Windows.Forms.ListBox();
            this.lbl_exhibitions = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Room.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(243, 8);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(138, 20);
            this.txt_Title.TabIndex = 0;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Location = new System.Drawing.Point(205, 10);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(27, 13);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Title";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 29);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(371, 304);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(383, 29);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(61, 21);
            this.btn_Browse.TabIndex = 3;
            this.btn_Browse.Text = "Browse...";
            this.btn_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(449, 31);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 20);
            this.textBox1.TabIndex = 4;
            // 
            // pnl_Room
            // 
            this.pnl_Room.Controls.Add(this.checkBox1);
            this.pnl_Room.Controls.Add(this.btn_DeleteRoom);
            this.pnl_Room.Controls.Add(this.lbx_Rooms);
            this.pnl_Room.Controls.Add(this.btn_AddRoom);
            this.pnl_Room.Controls.Add(this.lbl_ListRooms);
            this.pnl_Room.Location = new System.Drawing.Point(383, 54);
            this.pnl_Room.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_Room.Name = "pnl_Room";
            this.pnl_Room.Size = new System.Drawing.Size(213, 130);
            this.pnl_Room.TabIndex = 6;
            // 
            // btn_DeleteRoom
            // 
            this.btn_DeleteRoom.Location = new System.Drawing.Point(54, 77);
            this.btn_DeleteRoom.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DeleteRoom.Name = "btn_DeleteRoom";
            this.btn_DeleteRoom.Size = new System.Drawing.Size(50, 15);
            this.btn_DeleteRoom.TabIndex = 7;
            this.btn_DeleteRoom.Text = "Delete";
            this.btn_DeleteRoom.UseVisualStyleBackColor = true;
            this.btn_DeleteRoom.Click += new System.EventHandler(this.btn_DeleteRoom_Click);
            // 
            // lbx_Rooms
            // 
            this.lbx_Rooms.FormattingEnabled = true;
            this.lbx_Rooms.Location = new System.Drawing.Point(2, 18);
            this.lbx_Rooms.Margin = new System.Windows.Forms.Padding(2);
            this.lbx_Rooms.Name = "lbx_Rooms";
            this.lbx_Rooms.Size = new System.Drawing.Size(105, 56);
            this.lbx_Rooms.TabIndex = 2;
            // 
            // btn_AddRoom
            // 
            this.btn_AddRoom.Location = new System.Drawing.Point(0, 78);
            this.btn_AddRoom.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddRoom.Name = "btn_AddRoom";
            this.btn_AddRoom.Size = new System.Drawing.Size(50, 15);
            this.btn_AddRoom.TabIndex = 6;
            this.btn_AddRoom.Text = "Add";
            this.btn_AddRoom.UseVisualStyleBackColor = true;
            this.btn_AddRoom.Click += new System.EventHandler(this.btn_AddRoom_Click);
            // 
            // lbl_ListRooms
            // 
            this.lbl_ListRooms.AutoSize = true;
            this.lbl_ListRooms.Location = new System.Drawing.Point(2, 3);
            this.lbl_ListRooms.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_ListRooms.Name = "lbl_ListRooms";
            this.lbl_ListRooms.Size = new System.Drawing.Size(40, 13);
            this.lbl_ListRooms.TabIndex = 1;
            this.lbl_ListRooms.Text = "Rooms";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_deleteExhibition);
            this.panel1.Controls.Add(this.btn_AddExhibition);
            this.panel1.Controls.Add(this.lbx_exhibitions);
            this.panel1.Controls.Add(this.lbl_exhibitions);
            this.panel1.Location = new System.Drawing.Point(383, 188);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 153);
            this.panel1.TabIndex = 7;
            // 
            // btn_deleteExhibition
            // 
            this.btn_deleteExhibition.Location = new System.Drawing.Point(59, 79);
            this.btn_deleteExhibition.Margin = new System.Windows.Forms.Padding(2);
            this.btn_deleteExhibition.Name = "btn_deleteExhibition";
            this.btn_deleteExhibition.Size = new System.Drawing.Size(50, 15);
            this.btn_deleteExhibition.TabIndex = 5;
            this.btn_deleteExhibition.Text = "Delete";
            this.btn_deleteExhibition.UseVisualStyleBackColor = true;
            this.btn_deleteExhibition.Click += new System.EventHandler(this.btn_deleteExhibition_Click);
            // 
            // btn_AddExhibition
            // 
            this.btn_AddExhibition.Location = new System.Drawing.Point(5, 80);
            this.btn_AddExhibition.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddExhibition.Name = "btn_AddExhibition";
            this.btn_AddExhibition.Size = new System.Drawing.Size(50, 15);
            this.btn_AddExhibition.TabIndex = 4;
            this.btn_AddExhibition.Text = "Add";
            this.btn_AddExhibition.UseVisualStyleBackColor = true;
            this.btn_AddExhibition.Click += new System.EventHandler(this.btn_AddExhibition_Click);
            // 
            // lbx_exhibitions
            // 
            this.lbx_exhibitions.FormattingEnabled = true;
            this.lbx_exhibitions.Location = new System.Drawing.Point(5, 21);
            this.lbx_exhibitions.Margin = new System.Windows.Forms.Padding(2);
            this.lbx_exhibitions.Name = "lbx_exhibitions";
            this.lbx_exhibitions.Size = new System.Drawing.Size(105, 56);
            this.lbx_exhibitions.TabIndex = 3;
            this.lbx_exhibitions.SelectedIndexChanged += new System.EventHandler(this.lbx_exhibitions_SelectedIndexChanged);
            // 
            // lbl_exhibitions
            // 
            this.lbl_exhibitions.AutoSize = true;
            this.lbl_exhibitions.Location = new System.Drawing.Point(2, 6);
            this.lbl_exhibitions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_exhibitions.Name = "lbl_exhibitions";
            this.lbl_exhibitions.Size = new System.Drawing.Size(57, 13);
            this.lbl_exhibitions.TabIndex = 0;
            this.lbl_exhibitions.Text = "Exhibitions";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 98);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 341);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Room);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.txt_Title);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Room.ResumeLayout(false);
            this.pnl_Room.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pnl_Room;
        private System.Windows.Forms.ListBox lbx_Rooms;
        private System.Windows.Forms.Label lbl_ListRooms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_AddExhibition;
        private System.Windows.Forms.ListBox lbx_exhibitions;
        private System.Windows.Forms.Label lbl_exhibitions;
        private System.Windows.Forms.Button btn_deleteExhibition;
        private System.Windows.Forms.Button btn_DeleteRoom;
        private System.Windows.Forms.Button btn_AddRoom;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}


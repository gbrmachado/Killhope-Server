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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_DeleteRoom = new System.Windows.Forms.Button();
            this.lbx_Rooms = new System.Windows.Forms.ListBox();
            this.btn_AddRoom = new System.Windows.Forms.Button();
            this.lbl_ListRooms = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_deleteExhibition = new System.Windows.Forms.Button();
            this.btn_AddExhibition = new System.Windows.Forms.Button();
            this.lbx_exhibitions = new System.Windows.Forms.ListBox();
            this.lbl_exhibitions = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Room.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(364, 12);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(205, 26);
            this.txt_Title.TabIndex = 0;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Location = new System.Drawing.Point(308, 15);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(38, 20);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Title";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(976, 688);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Browse.Location = new System.Drawing.Point(3, 209);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(92, 32);
            this.btn_Browse.TabIndex = 3;
            this.btn_Browse.Text = "Browse...";
            this.btn_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(101, 209);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 26);
            this.textBox1.TabIndex = 4;
            // 
            // pnl_Room
            // 
            this.pnl_Room.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Room.Controls.Add(this.checkBox1);
            this.pnl_Room.Controls.Add(this.btn_DeleteRoom);
            this.pnl_Room.Controls.Add(this.lbx_Rooms);
            this.pnl_Room.Controls.Add(this.btn_AddRoom);
            this.pnl_Room.Controls.Add(this.lbl_ListRooms);
            this.pnl_Room.Location = new System.Drawing.Point(3, 3);
            this.pnl_Room.Name = "pnl_Room";
            this.pnl_Room.Size = new System.Drawing.Size(320, 200);
            this.pnl_Room.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(34, 151);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 24);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // btn_DeleteRoom
            // 
            this.btn_DeleteRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DeleteRoom.Location = new System.Drawing.Point(81, 118);
            this.btn_DeleteRoom.Name = "btn_DeleteRoom";
            this.btn_DeleteRoom.Size = new System.Drawing.Size(75, 23);
            this.btn_DeleteRoom.TabIndex = 7;
            this.btn_DeleteRoom.Text = "Delete";
            this.btn_DeleteRoom.UseVisualStyleBackColor = true;
            this.btn_DeleteRoom.Click += new System.EventHandler(this.btn_DeleteRoom_Click);
            // 
            // lbx_Rooms
            // 
            this.lbx_Rooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbx_Rooms.FormattingEnabled = true;
            this.lbx_Rooms.ItemHeight = 20;
            this.lbx_Rooms.Location = new System.Drawing.Point(3, 28);
            this.lbx_Rooms.Name = "lbx_Rooms";
            this.lbx_Rooms.Size = new System.Drawing.Size(156, 84);
            this.lbx_Rooms.TabIndex = 2;
            // 
            // btn_AddRoom
            // 
            this.btn_AddRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddRoom.Location = new System.Drawing.Point(0, 120);
            this.btn_AddRoom.Name = "btn_AddRoom";
            this.btn_AddRoom.Size = new System.Drawing.Size(75, 23);
            this.btn_AddRoom.TabIndex = 6;
            this.btn_AddRoom.Text = "Add";
            this.btn_AddRoom.UseVisualStyleBackColor = true;
            this.btn_AddRoom.Click += new System.EventHandler(this.btn_AddRoom_Click);
            // 
            // lbl_ListRooms
            // 
            this.lbl_ListRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ListRooms.AutoSize = true;
            this.lbl_ListRooms.Location = new System.Drawing.Point(3, 5);
            this.lbl_ListRooms.Name = "lbl_ListRooms";
            this.lbl_ListRooms.Size = new System.Drawing.Size(60, 20);
            this.lbl_ListRooms.TabIndex = 1;
            this.lbl_ListRooms.Text = "Rooms";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_deleteExhibition);
            this.panel1.Controls.Add(this.btn_AddExhibition);
            this.panel1.Controls.Add(this.lbx_exhibitions);
            this.panel1.Controls.Add(this.lbl_exhibitions);
            this.panel1.Location = new System.Drawing.Point(3, 247);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 237);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(81, 200);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_deleteExhibition
            // 
            this.btn_deleteExhibition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteExhibition.Location = new System.Drawing.Point(88, 122);
            this.btn_deleteExhibition.Name = "btn_deleteExhibition";
            this.btn_deleteExhibition.Size = new System.Drawing.Size(75, 23);
            this.btn_deleteExhibition.TabIndex = 5;
            this.btn_deleteExhibition.Text = "Delete";
            this.btn_deleteExhibition.UseVisualStyleBackColor = true;
            this.btn_deleteExhibition.Click += new System.EventHandler(this.btn_deleteExhibition_Click);
            // 
            // btn_AddExhibition
            // 
            this.btn_AddExhibition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddExhibition.Location = new System.Drawing.Point(8, 123);
            this.btn_AddExhibition.Name = "btn_AddExhibition";
            this.btn_AddExhibition.Size = new System.Drawing.Size(75, 23);
            this.btn_AddExhibition.TabIndex = 4;
            this.btn_AddExhibition.Text = "Add";
            this.btn_AddExhibition.UseVisualStyleBackColor = true;
            this.btn_AddExhibition.Click += new System.EventHandler(this.btn_AddExhibition_Click);
            // 
            // lbx_exhibitions
            // 
            this.lbx_exhibitions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbx_exhibitions.FormattingEnabled = true;
            this.lbx_exhibitions.ItemHeight = 20;
            this.lbx_exhibitions.Location = new System.Drawing.Point(8, 32);
            this.lbx_exhibitions.Name = "lbx_exhibitions";
            this.lbx_exhibitions.Size = new System.Drawing.Size(156, 84);
            this.lbx_exhibitions.TabIndex = 3;
            this.lbx_exhibitions.SelectedIndexChanged += new System.EventHandler(this.lbx_exhibitions_SelectedIndexChanged);
            // 
            // lbl_exhibitions
            // 
            this.lbl_exhibitions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_exhibitions.AutoSize = true;
            this.lbl_exhibitions.Location = new System.Drawing.Point(3, 9);
            this.lbl_exhibitions.Name = "lbl_exhibitions";
            this.lbl_exhibitions.Size = new System.Drawing.Size(85, 20);
            this.lbl_exhibitions.TabIndex = 0;
            this.lbl_exhibitions.Text = "Exhibitions";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.pnl_Room);
            this.flowLayoutPanel1.Controls.Add(this.btn_Browse);
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(981, 15);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(341, 498);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 741);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.txt_Title);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Room.ResumeLayout(false);
            this.pnl_Room.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}


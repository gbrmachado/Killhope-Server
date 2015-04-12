using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Room = Killhope.Plugins.Maps.Domain.JSON.Room;
using Point = Killhope.Plugins.Maps.Domain.JSON.Point;
using Exhibition = Killhope.Plugins.Maps.Domain.JSON.Exhibition;
using Killhope.Plugins.Maps.Domain;

namespace Killhope.Plugins.Maps.Presentation
{
    public partial class Form1 : Form
    {
        private MapImage im;

        //Adding a room or an exhibition is simple, simply left click on each point.
        //Right click = undo.
        //Double click for the final point to stop adding a room.

        //Click and drag is still move, so detect whether it is a click, or a drag.

        private ClickMode mode;

        private List<Point> points = new List<Point>();

        private ClickMode Mode
        {
            get { return this.mode; }
            set
            {
                if (value != ClickMode.Normal)
                    this.Cursor = Cursors.Cross;
                else
                    this.Cursor = Cursors.Default;
                this.mode = value;
            }
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var loc = @"C:\Users\David\Desktop\KILLHOPE_MAP.png";
            Image map = Image.FromFile(loc);

            im = new MapImage(map);

            AddExhibition(new Domain.JSON.Exhibition() { x = 50, y = 20 });
            AddRoom(new Domain.JSON.Room() { points = new List<Point>() { new Point(1, 2), new Point(10, 20), new Point(20, 20), new Point(20, 10) } });
            

            im.ImageChanged += Im_ImageChanged;

            btn_deleteExhibition.Enabled = lbx_exhibitions.SelectedItem != null;
        }

        private void AddRoom(Room room)
        {
            im.AddRoom(room);
            lbx_Rooms.Items.Add(room);
        }

        private void Im_ImageChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            im.Draw(e.Graphics);
        }

        private void btn_AddExhibition_Click(object sender, EventArgs e)
        {
            this.Mode = ClickMode.AddEhibtion;
        }

        private void AddExhibition(Exhibition e)
        {
            im.AddExhibition(e);
            lbx_exhibitions.Items.Add(e);
        }

        private enum ClickMode
        {
            Normal = 0,
            AddEhibtion = 1,
            AddRoom = 2
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && this.Mode == ClickMode.AddRoom)
            {
                if (!this.points.Any())
                    this.Mode = ClickMode.Normal;
                else
                    this.points.RemoveAt(this.points.Count - 1);

                return;
            }

            switch (this.Mode)
            {
                case ClickMode.AddEhibtion:
                    AddExhibition(new Exhibition() {  x = e.X, y = e.Y });
                    this.Mode = ClickMode.Normal;
                    break;
                case ClickMode.AddRoom:
                    this.points.Add(new Point(e.X, e.Y));
                    break;
                default:
                    break;
            }
        }


        private void btn_deleteExhibition_Click(object sender, EventArgs e)
        {
            var val = (Exhibition)lbx_exhibitions.SelectedItem;
            lbx_exhibitions.Items.Remove(val);
            im.DeleteExhibition(val);
        }

        private void lbx_exhibitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_deleteExhibition.Enabled = lbx_exhibitions.SelectedItem != null;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

            if (this.Mode != ClickMode.AddRoom)
                return;

            
            //Add a room based on the current points.
            if(this.points.Count > 2)
                AddRoom(new Room() { points = this.points.ToList() });
            this.points.Clear();
            this.Mode = ClickMode.Normal;
        }

        private void btn_AddRoom_Click(object sender, EventArgs e)
        {
            this.Mode = ClickMode.AddRoom;
        }

        private void btn_DeleteRoom_Click(object sender, EventArgs e)
        {
            var val = (Room)lbx_Rooms.SelectedItem;
            lbx_Rooms.Items.Remove(val);
            im.DeleteRoom(val);
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello world");
        }
    }
}

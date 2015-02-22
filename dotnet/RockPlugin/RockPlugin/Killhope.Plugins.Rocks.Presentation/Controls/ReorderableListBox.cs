using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Rocks.Presentation.Controls
{
    public partial class ReorderableListBox<T> : UserControl
    {
        public ReorderableListBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refreshes the enabled property of the up and down buttons
        /// </summary>
        private void ModifyUpDown()
        {
            int selectedIndex = lbx_Data.SelectedIndex;

            //TODO: do change on var.
            bool down = true && this.Enabled;
            bool up = true && this.Enabled;
            if (selectedIndex == -1)
            {
                down = false;
                up = false;
            }
            else if (selectedIndex == 0)
                up = false;
            //Not an else if because both may be true (0 == 0)
            if (selectedIndex == lbx_Data.Items.Count - 1)
                down = false;

            btn_Down.Enabled = down;
            btn_Up.Enabled = up;
        }

        private void notifiableListBox1_ItemsChanged(object sender, EventArgs e)
        {
            btn_Delete.Enabled = btn_Delete_Enabled;
            ModifyUpDown();
        }

        private void lbx_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyUpDown();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
                lbx_Data.Items.RemoveAt(SelectedIndex);
        }

        public void Add(T item)
        {
            this.lbx_Data.Items.Add(item);
        }

        public void MoveUp(int index)
        {
            if (index == 0)
                return;

            T @new = (T)lbx_Data.Items[index - 1];
            lbx_Data.Items[index - 1] = lbx_Data.Items[index];
            lbx_Data.Items[index] = @new;

            SelectedIndex--;
        }

        public void MoveDown(int index)
        {
            if (index == Count - 1)
                return;

            T @new = (T)lbx_Data.Items[index + 1];
            lbx_Data.Items[index + 1] = lbx_Data.Items[index];
            lbx_Data.Items[index] = @new;

            //Move the index to follow the change in items.
            SelectedIndex++;
        }

        public IEnumerable<T> Items
        {
            get
            {
                return lbx_Data.Items.Cast<T>();
            }
            set
            {
                //TODO: Probably throws events, also use AddRange
                lbx_Data.Items.Clear();
                if (value != null)
                    foreach (var item in value)
                        lbx_Data.Items.Add(item);
            }
        }

        public int Count { get { return lbx_Data.Items.Count; } }
        public int SelectedIndex { get { return lbx_Data.SelectedIndex; } set { lbx_Data.SelectedIndex = value; } }
        public object SelectedValue { get { return lbx_Data.SelectedItem; } }

        private void btn_Up_Click(object sender, EventArgs e)
        {
            MoveUp(SelectedIndex);
        }

        private void btn_Down_Click(object sender, EventArgs e)
        {
            MoveDown(SelectedIndex);
        }

        private bool btn_Delete_Enabled { get { return lbx_Data.Items.Count == 0 && SelectedIndex != -1 && this.Enabled; } }


        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.lbx_Data.Enabled = this.Enabled;
            this.btn_Delete.Enabled = btn_Delete_Enabled;
            ModifyUpDown();
        }
    }
}

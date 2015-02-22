using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Killhope.Plugins.Manager.Domain.Release.DTO;

namespace Killhope.Plugins.Manager.Presentation
{
    internal partial class Form1 : Form
    {





        private readonly FTPReleaseSideFactory leftSide;
        private readonly LocalTempSideFactory rightSide;
        private readonly MenuStripManager menuStripManager;

        public Form1(FTPReleaseSideFactory leftSide, LocalTempSideFactory rightSide)
        {
            this.menuStripManager= new MenuStripManager(this.menuStrip1);
            IEnumerable<IPlugin> plugins = ObtainPlugins();
            LoadMenuStrip(plugins, this.menuStripManager);
        }

        private IEnumerable<IPlugin> ObtainPlugins()
        {
            var res = new PluginLoader().ObtainPlugins();

            IEnumerable<IPlugin> allPlugins;
            try
            {
                allPlugins = res.GetExportedValues<IPlugin>();
            }
            catch (ImportCardinalityMismatchException e)
            {
                //No plugins were found.
                allPlugins = new List<IPlugin>();
            }
            return allPlugins;
        }

        private void LoadMenuStrip(IEnumerable<IPlugin> allPlugins, MenuStripManager m)
        {
            
            MenuItemManager m2 = new MenuItemManager();

            foreach (var plugin in allPlugins)
            {
                foreach (var menuItem in plugin.MenuItems)
                {
                    AddMenuItem(m2, menuItem);

                }
            }

            m.Merge(m2);
        }

        private void AddMenuItem(MenuItemManager m2, Tuple<Action, ItemLocation> tuple)
        {
            m2.AddMenuItem(new NamedMenuItem(tuple.Item2, tuple.Item1));
        }

        public Form1()
        {
            InitializeComponent();
        }


        public void Upload()
        {
            //presumably we have a TempLocalSide
            ReleaseSide right = rightSide.GetInstance();
            ReleaseSide left = leftSide.GetInstance();

            new ReleaseInformation(left, right);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Upload();
        }
    }
}

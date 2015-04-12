using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release;
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
    public partial class DiffDisplay : Form
    {

        /*
         * Aims: Both boxes display location and file name
         * On selection where BOTH have the same location, they will both be 
         * 
         * 
         * 
         */
        private ReleaseInformation model;

        public DiffDisplay()
        {
            InitializeComponent();

            //FTPReleaseSide ftp = new FTPReleaseSide();


        }
    }
}

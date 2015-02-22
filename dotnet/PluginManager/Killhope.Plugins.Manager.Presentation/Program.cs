using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain.Release.DTO;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;
using Killhope.Plugins.Manager.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var all = new PluginLoader().ObtainPlugins();

            IFileSystemService systemService = new LocalFileSystemManager("TODO");
            var man = new SiteManifestManager(systemService);
            var siteman = new LocalSiteManager(systemService, new ItemLocation("", ""));
            
            FTPReleaseSideFactory factory = new FTPReleaseSideFactory(man, siteman, null);


            //TODO: Versions and upgrade
            if (Settings.Default.IsFirstRun)
                PerformFirstRun();


            LocalTempSideFactory f = new LocalTempSideFactory(new LocalReleaseManager(systemService, null));

            Settings.Default.IsFirstRun = false;
            Settings.Default.Save();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(factory, f));
        }

        private static LocalReleaseManager GetReleaseManager()
        {
            var filesystemManager = new LocalFileSystemManager(Settings.Default.ReleaseFolder);
            return new LocalReleaseManager(filesystemManager, new ItemLocation("", ""));
        }

        private static void PerformFirstRun()
        {
            
            var localsystem = GetReleaseManager();
            localsystem.CreateRelease(GetInitialRelease());
        }

        private static ReleaseDTO GetInitialRelease()
        {
            return ReleaseDTO.CreateNew(DateTime.Now);
        }


    }
}

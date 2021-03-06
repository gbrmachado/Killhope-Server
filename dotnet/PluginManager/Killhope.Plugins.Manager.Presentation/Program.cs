﻿using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain.Release.DTO;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;
using Killhope.Plugins.Manager.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Killhope.Plugins.Manager.Presentation
{
    static class Program
    {
        private static LocalReleaseManager releaseManager;


        private static IFileSystemService GetLocalFTPFileSystemService()
        {
            var ret = new LocalFileSystemManager(Settings.Default.LocalFTPLocation);
            ret.Validate().ThowIfInvalid();
            return ret;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var all = new PluginLoader().ObtainPlugins();

            IFileSystemService systemService = GetLocalFTPFileSystemService();
            var man = new SiteManifestManager(systemService);
            var siteman = new LocalSiteManager(systemService, new ItemLocation("", ""));
            
            FTPReleaseSideFactory factory = new FTPReleaseSideFactory(man, siteman);

            DEBUG_ResetSettings();

            //TODO: Versions and upgrade
            if (Settings.Default.IsFirstRun)
                PerformFirstRun();
            else
                Initialise();

            if (Program.releaseManager == null)
                throw new InvalidOperationException("releaseManager is invalid.");

            LocalTempSideFactory f = new LocalTempSideFactory(Program.releaseManager);

            Settings.Default.IsFirstRun = false;
            Settings.Default.Save();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(factory, f));
        }

        [Conditional("DEBUG")]
        private static void DEBUG_ResetSettings()
        {
            Settings.Default.Reset();
            Settings.Default.Reset();
        }

        private static void Initialise()
        {
            throw new NotImplementedException();
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
            Program.releaseManager = localsystem;
        }

        private static ReleaseDTO GetInitialRelease()
        {
            return ReleaseDTO.CreateNew(DateTime.Now);
        }


    }
}

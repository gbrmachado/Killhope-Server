using Killhope.Plugins.Manager.Domain.Release;
using System;
using Killhope.Plugins.Manager.Domain;
using System.Windows.Forms;
using Utilities.FTP;
using Killhope.Plugins.Manager.Presentation.Properties;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;
using System.ComponentModel.Composition;
using Killhope.Plugins.Manager.Domain.Release.FTP;
using System.Diagnostics;
using Killhope.Plugins.Manager.Domain.Release.DTO;

namespace Killhope.Plugins.Manager.Presentation
{
    [Export(typeof(FTPReleaseSideFactory))]
    public partial class FTPReleaseSideFactory : ReleaseSideFactory
    {
        private readonly SiteManifestManager manifestManager;
        private readonly LocalSiteManager localSiteManager;

        [ImportingConstructor]
        public FTPReleaseSideFactory(SiteManifestManager manager, LocalSiteManager localSiteManager)
        {
            this.manifestManager = manager;
            this.localSiteManager = localSiteManager;
        }

        /// <summary>
        /// Allows the initial data in the form to be populated with values which can be excluded from source control with no loss.
        /// </summary>
        /// <param name="f">The form to populate with data</param>
        [Conditional("DEBUG")]
        partial void PopulateData(SiteSelection f);


        public override ReleaseSide GetInstance()
        {
            //TODO: Yeah, this code sucks.

            //Determine the FTP Server to view.
            SiteSelection f = new SiteSelection(manifestManager);
            PopulateData(f);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.Cancel)
                return null;

            var manifest = f.Result;


            IFTPClient client = getFTPClient(manifest, f.PasswordResult);
            //Determine the release to view from the FTP as comparison.

            var releaseIDResult = getReleaseNumber(client);
            if (!releaseIDResult.Item1)
                return null;

            int releaseNumber = releaseIDResult.Item2;

            string ftpFolderLocation = Settings.Default.LocalFTPLocation;

            
            string releaseFolderPath = LocalFTPServiceUtilities.GetFolderPathForSite(ftpFolderLocation, manifest.FTPServer);

            throw new NotImplementedException();
            //TODO: localSiteManager should be instantiated with the value of the site here.
            LocalSiteManager siteManager2 = new LocalSiteManager(null, null);

            LocalFTPSiteCache cache = new LocalFTPSiteCache(releaseFolderPath, localSiteManager);


            return new FTPReleaseSide(client, cache, releaseNumber);
        }

        private IFTPClient getFTPClient(SiteManifest manifest, string password)
        {
            var client = new FTPclient(manifest.FTPServer, manifest.DefaultUserName, password, true);
            string path = manifest.FTPPath;
            if (!path.StartsWith("/"))
                path = "/" + path;
            client.CurrentDirectory = path;
            FTPClientValidationDecorator d = new FTPClientValidationDecorator(client);
            return d;
        }

        private Tuple<bool, int> getReleaseNumber(IFTPClient client)
        {
            var picker = new ReleasePicker(FTPUtilities.GetAvailableReleases(client));

            picker.ShowDialog();

            return new Tuple<bool, int>(picker.DialogResult == DialogResult.OK, picker.Result);
        }
    }
}

using Killhope.Plugins.Manager.Domain.Release;
using System;
using Killhope.Plugins.Manager.Domain;
using System.Windows.Forms;
using Utilities.FTP;
using Killhope.Plugins.Manager.Presentation.Properties;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;
using System.ComponentModel.Composition;

namespace Killhope.Plugins.Manager.Presentation
{
    [Export(typeof(FTPReleaseSideFactory))]
    public class FTPReleaseSideFactory : ReleaseSideFactory
    {
        private readonly SiteManifestManager manifestManager;
        private readonly LocalSiteManager localSiteManager;
        private readonly ILocalFTPService service;

        [ImportingConstructor]
        public FTPReleaseSideFactory(SiteManifestManager manager, LocalSiteManager localSiteManager, ILocalFTPService service)
        {
            this.manifestManager = manager;
            this.localSiteManager = localSiteManager;
            this.service = service;
        }

        public override ReleaseSide GetInstance()
        {
            //TODO: Yeah, this code sucks.

            //Determine the FTP Server to view.
            SiteSelection f = new SiteSelection(manifestManager);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.Cancel)
                return null;

            var manifest = f.Result;


            FTPclient client = new FTPclient(manifest.FTPPath, manifest.DefaultUserName, f.PasswordResult, true);

            //Determine the release to view from the FTP as comparison.

            var releaseIDResult = getReleaseNumber(client);
            if (!releaseIDResult.Item1)
                return null;

            int releaseNumber = releaseIDResult.Item2;

            string ftpFolderLocation = Settings.Default.LocalFTPLocation;

            
            string releaseFolderPath = service.getFolderPathForSite(ftpFolderLocation, manifest.FTPServer);

            throw new NotImplementedException();
            //TODO: localSiteManager should be instantiated with the value of the site here.
            LocalSiteManager siteManager2 = new LocalSiteManager(null, null);

            LocalFTPSiteCache cache = new LocalFTPSiteCache(releaseFolderPath, localSiteManager);


            return new FTPReleaseSide(client, cache, releaseNumber);
        }

        private Tuple<bool, int> getReleaseNumber(FTPclient client)
        {
            var picker = new ReleasePicker(FTPUtilities.GetAvailableReleases(client));

            picker.ShowDialog();

            return new Tuple<bool, int>(picker.DialogResult == DialogResult.OK, picker.Result);
        }
    }
}

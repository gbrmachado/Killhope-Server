using Killhope.Plugins.Manager.Domain.Release.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Utilities.FTP;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// An abstraction over a single existing release on a single external server.
    /// Handles provision of files from this release an synchronisation to a local folder (if necessary).
    /// </summary>
    public class FTPReleaseSide : ReleaseSide
    {
        private readonly IFTPClient client;
        private string rootFolder = "/";
        private readonly LocalFTPSiteCache localFTPCache;
        private readonly int releaseNumber;
        private LocalFTPSiteCache localFTPSiteCache;


        /// <summary>
        /// The path to the folder containing all releases on the server
        /// </summary>
        public string RootFolder { 
            get { return rootFolder; } 
            private set 
            {
                if (value == null)
                    rootFolder = "/";
                else if (!value.StartsWith("/"))
                    rootFolder = "/" + value;
            } 
        }

        //TODO: Probably move the FTPClient logic outside of this class, even though it doesn't hit the network on ctor.

        public FTPReleaseSide(SiteManifest manifest, string password, LocalFTPSiteCache cache, int releaseNumber) : this(manifest.FTPServer, new NetworkCredential(manifest.DefaultUserName, password), manifest.FTPPath, cache, releaseNumber)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="details"></param>
        /// <param name="rootFolder">The path to the folder containing all releases on the server</param>
        /// <param name="cache">A manager for the release on the local file system.</param>
        /// <param name="releaseNumber">The release which should be viewed.</param>
        public FTPReleaseSide(string server, NetworkCredential details, string rootFolder, LocalFTPSiteCache cache, int releaseNumber)
        {
            bool ssl = true;
            if (details == null)
            {
                details = new NetworkCredential((string)null, (string)null);
                ssl = false;
            }

            client = new FTPclient(server, details.UserName, details.Password, true);

            client.EnableSSL = ssl;

            this.RootFolder = rootFolder;
            this.releaseNumber = releaseNumber;
            this.localFTPCache = cache;
        }

        public FTPReleaseSide(IFTPClient client, LocalFTPSiteCache localFTPSiteCache, int releaseNumber)
        {
            this.client = client;
            this.client.EnableSSL = String.IsNullOrEmpty(client.Password);
            this.localFTPSiteCache = localFTPSiteCache;
            this.releaseNumber = releaseNumber;
        }

        public override bool CanAdd
        {
            get { return false; }
        }

        public override bool CanRemove
        {
            get { return false; }
        }

        public override bool CanRefresh
        {
            get { return true; }
        }

        public override void InitialLoad()
        {
            if (currentReleaseIsLocal())
                Refresh();

            if (!currentReleaseIsLocal())
                throw new InvalidOperationException("Refresh operation did not correctly transfer files to local disk.");

            Refresh();
        }

        /// <summary>
        /// Whether the currently selected release exists locally on the file system.
        /// </summary>
        /// <returns></returns>
        private bool currentReleaseIsLocal()
        {
            if (!this.localFTPCache.IsReleaseAvailable(this.releaseNumber))
                return false;

            ReleaseDTO release = localFTPCache.GetRelease(this.releaseNumber);

            this.ContainedFiles = release.Files;

            //TODO: Propogating the release status (release.Manifest.ReleaseStatus)
            return true;
        }

        public override void Refresh()
        {
            ReleaseManifestDTO r = this.getManifest();
            this.Refresh(this.getManifest());
        }

        public string CurrentDirectory
        {
            get { return this.RootFolder + this.releaseNumber; }
        }

        /// <summary>
        /// Contacts the remote server and re-downloads the specific release specified in the manifest, overwriting the current data.
        /// </summary>
        /// <param name="manifest"></param>
        private void Refresh(ReleaseManifestDTO manifest)
        {
            client.CurrentDirectory = CurrentDirectory;

            //The manifest contains the version number on the server. 
            //This should match the folder name, if it doesn't, an invalid operation has occurred.
            if (manifest.Version != this.releaseNumber)
                throw new InvalidOperationException($"Server folder: {this.RootFolder} {this.releaseNumber} does not match version found in manifest: {manifest.Version}.");


            //TODO: This code is performance-sensitive and would be a good candidate to modify to use the async/await API.
            //Sadly FTPclient does not support this yet.
            //If we fix this, we could speed it up immensely.

            List<string> files = getFileNamesRecursively("").ToList();

            var memoryFiles = from path in files
                              select
                                 new InMemoryFile(ItemLocation.FromRelativeFilePath(path), client.DownloadFile(path));

            this.ContainedFiles = this.SaveFiles(manifest, memoryFiles);
        }

        private IEnumerable<File> SaveFiles(ReleaseManifestDTO manifest, IEnumerable<InMemoryFile> memoryFiles)
        {
            return SaveRelease(new ReleaseDTO(manifest, memoryFiles));
        }

        private IEnumerable<File> SaveRelease(ReleaseDTO release)
        {
            return this.localFTPCache.CreateRelease(release);
        }

        private ReleaseManifestDTO getManifest()
        {
            return ReleaseManifestDTO.FromStream(client.DownloadFile(ReleaseManifestDTO.FileName));
        }

        private IEnumerable<string> getFileNamesRecursively(string folder)
        {
            FTPdirectory folders = null;
            try
            {
                folders = client.ListDirectoryDetail(folder);
            }
            catch (WebException ex)
            {
                //550 errors are likely.
                //TODO: Log error.
                return new List<string>();
            }
            List<string> files = new List<string>();
            foreach (var file in folders.GetFiles())
                files.Add(file.FullName);

            foreach (var dir in folders.GetDirectories())
                files = files.Concat(getFileNamesRecursively(dir.FullName)).ToList();

            return files;
        }



        /// <summary>
        /// Add Operation is not supported for FTP
        /// </summary>
        /// <param name="file"></param>
        public override void Add(File file)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Remove Operation is not supported for FTP
        /// </summary>
        /// <param name="fileLocation"></param>
        public override void Remove(ItemLocation fileLocation)
        {
            throw new NotSupportedException();
        }
    }
}

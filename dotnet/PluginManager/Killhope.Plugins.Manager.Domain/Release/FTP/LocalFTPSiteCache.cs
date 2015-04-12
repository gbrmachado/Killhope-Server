using Killhope.Plugins.Manager.Domain.Release.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// Information pertaining to releases locally on the disk from a specific domain.
    /// </summary>
    public class LocalFTPSiteCache
    {
        /*
         * This application may place different releases of the application on different servers.
         * As it allows a diff operation, it must also allow for the download of a release for comparison.
         * 
         * This handles the multitude of releases that may be available.
         */
        private readonly string path;
        private readonly LocalSiteManager folderViewer;
        private IEnumerable<int> availableReleases;


        public int NumberOfReleases { get { return AvailableReleases.Count(); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="releasePath">The fully qualified path to the local FTP download folder for the specific site.</param>
        /// <param name="folderViewer"></param>
        public LocalFTPSiteCache(string releasePath, LocalSiteManager folderViewer)
        {
            if (releasePath == null)
                throw new ArgumentNullException("releasePath");

            if (folderViewer == null)
                throw new ArgumentNullException("folderViewer");


            this.path = releasePath;
            this.folderViewer = folderViewer;
        }

        /// <summary>
        /// Returns the most recently posted release. Throws an error if no releases have been input.
        /// </summary>
        public int GetLatestRelease()
        {
            return AvailableReleases.Max();
        }

        public IEnumerable<int> AvailableReleases
        {
            get
            {
                if (availableReleases == null)
                    Refresh();
                return availableReleases;
            }
            private set
            {
                availableReleases = value;
            }
        }

        //private string domainName;

        ///// <summary>
        ///// The fully qualified domain that the Release came from.
        ///// </summary>
        //public string Domain
        //{
        //    get
        //    {
        //        if (domainName == null)
        //            Refresh();
        //        return domainName;
        //    }
        //    private set
        //    {
        //        domainName = value;
        //    }
        //}

        /// <summary>
        /// Browses the specified folder for changes.
        /// </summary>
        public void Refresh()
        {
            //TODO: Metadata instead of looking for folder names.

            int DO_NOT_USE = 0;
            this.AvailableReleases = from folderName in getAvailableFolders()
                                          where int.TryParse(folderName, out DO_NOT_USE)
                                          select DO_NOT_USE;


           // this.Domain = folderViewer.GetDomainName(this.path);
        }

        private IEnumerable<string> getAvailableFolders()
        {
            var ret = folderViewer.GetAvailableFolders(this.path);
            if (ret == null)
                throw new InvalidOperationException(String.Format("the specified folerViewer: {0} returned an invalid result (null)", folderViewer));

            return ret;

        }

        public bool IsReleaseAvailable(int release)
        {
            if (release <= 0 || release >= NumberOfReleases)
                throw new ArgumentException(String.Format("Invalid release: {0}", release));

            return AvailableReleases.Contains(release);
        }

        public ReleaseDTO GetRelease(int releaseNumber)
        {
            return folderViewer.GetRelease(releaseNumber);
        }

        internal IEnumerable<File> CreateRelease(ReleaseDTO release)
        {
            return folderViewer.CreateRelease(release);
        }
    }
}

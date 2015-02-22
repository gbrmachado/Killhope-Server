using Killhope.Plugins.Manager.Domain.Release.DTO;
using Killhope.Plugins.Manager.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// Responsible for the loading and saving of all sites and managing duplicates.
    /// </summary>
    public class LocalFTPManager
    {
        private readonly IFileSystemService fileViewer;

        public LocalFTPManager(IFileSystemService fileViewer)
        {
            //TODO: FolderViewer is not the correct class for this level of abstraction.
            this.fileViewer = fileViewer;
        }

        public IEnumerable<string> AvailableSites {
            get
            {
                return from siteLocation in this.fileViewer.GetValidSites() select siteLocation.Folders.Last();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifest"></param>
        /// <exception cref="ArgumentNullException">The provided manifest was null.</exception>
        /// <exception cref="ArgumentException">The provided manifest failed validation.</exception>
        /// <exception cref="InvalidOperationException">A site wth the same domain name and path already exists.</exception>
        public void AddSite(SiteManifest manifest)
        {
            if (manifest == null)
                throw new ArgumentNullException(nameof(manifest));

            if (!manifest.Validate().isValid)
                throw new ArgumentException("Manifest is not valid for saving.");

            //TODO: Check for duplicates.
            string potentialName = GenerateName(manifest);
            if (AvailableSites.Contains(potentialName))
                throw new InvalidOperationException($"A site with the name: {potentialName} already exists.");

            var folder = new ItemLocation(potentialName, "");
            this.fileViewer.CreateFolder(folder);
            new SiteManifestManager(this.fileViewer).CreateManifest(folder, manifest);


        }

        /// <summary>
        /// Removes illegal characters from the manifest name.
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private string GenerateName(SiteManifest manifest)
        {
            //http://stackoverflow.com/a/7393722
            string invalidName = manifest.FTPServer + " " + manifest.FTPPath;
            return Path.GetInvalidFileNameChars().Aggregate(invalidName, (current, c) => current.Replace(c.ToString(), " "));
        }

        public void RemoveSite(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException($"The folder name specified: {folderName} is invalid.");

            var folderToDelete = new ItemLocation(folderName, "");
            this.fileViewer.RemoveFolder(folderToDelete);
        }


        public LocalFTPSiteCache GetSiteInformation(string folderName)
        {
            return new LocalFTPSiteCache(folderName, new LocalSiteManager(this.fileViewer, new ItemLocation(folderName,"")));
        }


    }
}

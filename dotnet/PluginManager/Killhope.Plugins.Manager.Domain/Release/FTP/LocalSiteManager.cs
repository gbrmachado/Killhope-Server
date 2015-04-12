using System.Collections.Generic;
using System.Linq;
using Killhope.Plugins.Manager.Domain.Release.DTO;
using System;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// Allows operations to be performed on a local version of files obtained from an FTP server.
    /// </summary>
    public class LocalSiteManager
    {
        private readonly LocalReleaseManager releaseManager;
        private readonly ItemLocation siteLocation;
        private readonly IFileSystemService fileSystemService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filesystemService"></param>
        /// <param name="siteLocation">The relative path to the root of the FTP container folder where the specific site is kept.</param>
        public LocalSiteManager(IFileSystemService filesystemService, ItemLocation siteLocation)
        {
            this.releaseManager = new LocalReleaseManager(filesystemService, siteLocation);
            this.siteLocation = siteLocation;
            this.fileSystemService = filesystemService;
        }


        public IEnumerable<string> GetAvailableFolders(string path)
        {
            return from folder 
                   in fileSystemService.GetFolders(new ItemLocation(path,""))
                   select folder.Folders.Last();
        }

        public ReleaseDTO GetRelease(int releaseNumber)
        {
            releaseManager.ReleaseLocation = GetReleaseLocation(releaseNumber);
            return releaseManager.GetRelease();
        }


        private ItemLocation GetReleaseLocation(int releaseNumber)
        {
            return siteLocation.AddFolder(releaseNumber.ToString());
        }

        /// <summary>
        /// Deletes the specified release from the local file system.
        /// </summary>
        /// <param name="releaseNumber">The version of the release to delete</param>
        public void DeleteRelease(int releaseNumber)
        {
            releaseManager.ReleaseLocation = GetReleaseLocation(releaseNumber);
            releaseManager.DeleteRelease();
        }



        /// <summary>
        /// Allows creation of a file inside a release. Aimed to 
        /// </summary>
        /// <param name="releaseNumber"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerable<File> CreateRelease(ReleaseDTO release)
        {
            releaseManager.ReleaseLocation = GetReleaseLocation(release.Version);
            return releaseManager.CreateRelease(release);
        }

    }
}

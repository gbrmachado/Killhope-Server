using Killhope.Plugins.Manager.Domain.Release.DTO;
using System.Collections.Generic;
using System;

namespace Killhope.Plugins.Manager.Domain.Release.Local_IO
{
    public class LocalReleaseManager
    {
        private readonly IFileSystemService fileSystemService;
        public ItemLocation ReleaseLocation { get; set; }

        public LocalReleaseManager(IFileSystemService filesystemService, ItemLocation releaseLocation)
        {
            if (filesystemService == null)
                throw new ArgumentNullException(nameof(filesystemService));

            if (releaseLocation == null)
                throw new ArgumentNullException(nameof(releaseLocation));

            this.fileSystemService = filesystemService;
            this.ReleaseLocation = releaseLocation;
        }


        public ReleaseDTO GetRelease()
        {
            ReleaseManifestDTO manifest = getManifest(ReleaseLocation);
            return new ReleaseDTO(manifest, this.getFilesFromRelease());
        }

        /// <summary>
        /// Deletes the specified release from the local file system.
        /// </summary>
        /// <param name="releaseNumber">The version of the release to delete</param>
        public void DeleteRelease()
        {
            this.fileSystemService.RemoveFolder(ReleaseLocation);
        }

        public File AddFileToRelease(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            File toSave = new InMemoryFile(GetLocation(file.Location), file.GetContent());

            return this.fileSystemService.SaveFile(toSave);
        }


        //TODO: Rename
        private ItemLocation GetLocation(ItemLocation location)
        {
            return this.ReleaseLocation.Append(location);
        }

        public void RemoveFileFromRelease(ItemLocation fileLocation)
        {
            var loc = GetLocation(fileLocation);
            this.fileSystemService.RemoveFile(loc);
        }



        /// <summary>
        /// Allows creation of a file inside a release. Aimed to 
        /// </summary>
        /// <param name="releaseNumber"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerable<File> CreateRelease(ReleaseDTO release)
        {
            this.fileSystemService.CreateFolder(ReleaseLocation);

            SaveManifest(release);

            List<File> files = new List<File>();
            foreach (var file in release.Files)
            {
                files.Add(this.fileSystemService.SaveFile(file));
            }
            return files;
        }

        private void SaveManifest(ReleaseDTO release)
        {
            var manifestLocation = ReleaseLocation.SetName(ReleaseManifestDTO.FileName);
            var file = new InMemoryFile(manifestLocation, release.Manifest.ToByteArray());
            this.fileSystemService.SaveFile(file);
        }

        private IEnumerable<File> getFilesFromRelease()
        {
            return this.fileSystemService.GetFilesRecursively(ReleaseLocation);
        }

        private ReleaseManifestDTO getManifest(ItemLocation location)
        {
            var manifest = location.SetName(ReleaseManifestDTO.FileName);
            return ReleaseManifestDTO.FromByteArray(this.fileSystemService.GetFile(manifest).GetContent());
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace Killhope.Plugins.Manager.Domain.Release.DTO
{
    /// <summary>
    /// A Collection of metadata and files associated with a release.
    /// </summary>
    public class ReleaseDTO
    {
        public IEnumerable<File> Files { get; set; }
        public ReleaseManifestDTO Manifest { get; set; }

        public ReleaseDTO(ReleaseManifestDTO manifest, IEnumerable<File> files)
        {
            this.Manifest = manifest;
            this.Files = files;
        }


        private IEnumerable<ItemLocation> Locations { get { return from f in Files select f.Location; }}

        public int Version { get { return Manifest.Version; } }

        internal IEnumerable<ItemLocation> GetFolderNames()
        {
            return ItemLocation.GetDistinctFolders(Locations);
        }

        public static ReleaseDTO CreateNew(DateTime now)
        {
            return new ReleaseDTO(new ReleaseManifestDTO()
            {
                Date = now,
                NumberOfFiles = 0,
                ReleaseStatus = ReleaseManifestDTO.Status.Invalid,
                Version = 1
            }
                  , new File[] { });
        }
    }
}

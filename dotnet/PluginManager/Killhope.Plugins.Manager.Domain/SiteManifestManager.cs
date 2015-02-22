using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain.Release.DTO;
using System.Collections.Generic;
using System;
using System.IO;
using System.ComponentModel.Composition;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// Responsible for the saving, loading and updating of manifests for FTP Sites.
    /// </summary>
    [Export(typeof(SiteManifestManager))]
    public class SiteManifestManager
    {
        internal static readonly string ManifestName = "siteManifest.json";

        private readonly IFileSystemService fileSystemService;

        [ImportingConstructor]
        public SiteManifestManager(IFileSystemService fileSystemService)
        {
            this.fileSystemService = fileSystemService;
        }

        public IEnumerable<SiteManifest> GetManifests()
        {
            List<SiteManifest> manifests = new List<SiteManifest>();

            //look through each folder from the root and determine if it has a manifest. 
            //If it does then it is a site (and add it to the list).

            var folders = fileSystemService.GetFolders(ItemLocation.Root);
            foreach(var folder in folders)
            {
                var potentialFile = folder.SetName(ManifestName);
                if(fileSystemService.ContainsFile(potentialFile))
                {
                    try
                    {
                        manifests.Add(LoadFile(potentialFile));
                    }
                    catch (Exception e)
                    {
                        //TODO: Log
                    }
                }
            }

            return manifests;

        }

        internal void CreateManifest(ItemLocation folder, SiteManifest manifest)
        {
            var file = new InMemoryFile(folder, new MemoryStream(manifest.ToByteArray()));

            fileSystemService.SaveFile(file);
        }

        private SiteManifest LoadFile(ItemLocation potentialFile)
        {
            if (potentialFile == null)
                throw new ArgumentNullException(nameof(potentialFile));

            var manifest = fileSystemService.GetFile(potentialFile);

            return SiteManifest.FromByteStream(manifest.GetContent());
        }
    }
}
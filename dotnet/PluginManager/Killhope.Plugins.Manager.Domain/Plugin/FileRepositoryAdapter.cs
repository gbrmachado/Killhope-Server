using Killhope.Plugins.Manager.Domain.Release;
using System;
using System.ComponentModel.Composition;

namespace Killhope.Plugins.Manager.Domain.Plugin
{
    /// <summary>
    /// An adapter from a ILocalReleaseManager to an IFileRepository
    /// </summary>
    [Export(typeof(IFileRepository))]
    internal class FileRepositoryAdapter : IFileRepository
    {
        private readonly IFileSystemService releaseManager;

        public FileRepositoryAdapter(IFileSystemService releaseManager)
        {
            if (releaseManager == null)
                throw new ArgumentNullException(nameof(releaseManager));

            this.releaseManager = releaseManager;
        }

        public File Load(ItemLocation location)
        {
            return releaseManager.GetFile(location);
        }

        public void Save(File file)
        {
            releaseManager.SaveFile(file);
        }

        public void Delete(ItemLocation location)
        {
            releaseManager.RemoveFile(location);
        }
    }
}

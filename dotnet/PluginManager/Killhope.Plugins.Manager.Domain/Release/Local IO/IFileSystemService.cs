using Killhope.Plugins.Manager.Presentation;
using System.Collections.Generic;
using System.Linq;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// A service to access files on the file system rooted at a specific directory.
    /// </summary>
    public interface IFileSystemService
    {
        File GetFile(ItemLocation fileLocation);
        File SaveFile(File toSave);
        void RemoveFile(ItemLocation fileLocation);
        bool ContainsFile(ItemLocation potentialFile);

        IEnumerable<File> ListFiles(ItemLocation itemLocation);
        IEnumerable<ItemLocation> GetFolders(ItemLocation root);

        void CreateFolder(ItemLocation folder);
        void RemoveFolder(ItemLocation folderToDelete);
    }

    public static class IFileSystemServiceExtensions
    {
        public static IEnumerable<ItemLocation> GetValidSites(this IFileSystemService fileSystemService)
        {

            List<ItemLocation> ret = new List<ItemLocation>();
            var folders = fileSystemService.GetFolders(ItemLocation.Root);
            foreach (var folder in folders)
            {
                var potentialFile = folder.SetName(SiteManifestManager.ManifestName);
                if (fileSystemService.ContainsFile(potentialFile))
                    ret.Add(potentialFile);
            }

            return ret;
        }

        public static IEnumerable<File> GetFilesRecursively(this IFileSystemService fileSystemService, ItemLocation itemLocation)
        {
            List<File> files = fileSystemService.ListFiles(itemLocation).ToList();
            var filesInFolders = from folder 
                                 in fileSystemService.GetFolders(itemLocation)
                                 select GetFilesRecursively(fileSystemService, folder);

            foreach (var foldersList in filesInFolders)
                files.AddRange(foldersList);

            return files;
        }
    }
}

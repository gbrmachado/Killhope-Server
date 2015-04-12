using Killhope.Plugins.Manager.Domain.Release.DTO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using IOPath = System.IO.Path;
using System;
using System.Diagnostics;
using Killhope.Plugins.Rocks.Domain.Application;

namespace Killhope.Plugins.Manager.Domain.Release
{
    public class LocalFileSystemManager : IFileSystemService
    {

        public string Path { get; private set; }

        public LocalFileSystemManager(string path)
        {
            if (!path.EndsWith(IOPath.DirectorySeparatorChar.ToString()))
                path = path + IOPath.DirectorySeparatorChar;
            this.Path = path;
        }

        /// <summary>
        /// Whether the curent FileSystemManager can be used.
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
        {
            var ret = new ValidationResult();
            ret.AddIf(!Directory.Exists(this.Path), $"The supplied path: \"{Path}\" is invalid.");
            return ret;
        }

        private ReleaseManifestDTO getManifest()
        {
            ItemLocation manifestLocation = new ItemLocation("", ReleaseManifestDTO.FileName);
            File manifestFile = GetFile(manifestLocation);
            byte[] content = manifestFile.GetContent();
            ReleaseManifestDTO manifest = ReleaseManifestDTO.FromString(Encoding.Unicode.GetString(content));
            return manifest;
        }

        public ReleaseDTO GetRelease()
        {
            ReleaseManifestDTO manifest = getManifest();
            IEnumerable<File> files = getAllFiles(new List<string> { });

            return new ReleaseDTO(manifest, files);
        }

        public static string BuildPath(string original, IEnumerable<string> currentPath)
        {
            var toAdd = string.Join(IOPath.DirectorySeparatorChar.ToString(), currentPath);
            if (toAdd.Length == 0)
                return original;

            return original + toAdd + IOPath.DirectorySeparatorChar;
        }



        private IEnumerable<LocalFile> getAllFiles(IEnumerable<string> currentRelativePath)
        {

            List<LocalFile> ret = new List<LocalFile>();

            string path = BuildPath(this.Path, currentRelativePath);

            string[] fullPaths = Directory.GetFiles(path);

            ItemLocation loc = ItemLocation.FromFolderTree(currentRelativePath);

            foreach (string fpath in fullPaths)
                ret.Add(new LocalFile(loc.SetName(IOPath.GetFileName(fpath)), fpath));

            //recursive call.
            foreach (var dir in Directory.GetDirectories(path))
                ret.AddRange(getAllFiles(currentRelativePath.Concat(new[] { dir })));

            return ret;
        }

        public File GetFile(ItemLocation fileLocation)
        {
            return new LocalFile(fileLocation, CombinePaths(fileLocation));
        }

        public void SaveFile(File toSave)
        {
            string path = CombinePaths(toSave);
            System.IO.File.WriteAllBytes(path, toSave.GetContent());
        }

        public void RemoveFile(ItemLocation fileLocation)
        {
            string path = CombinePaths(fileLocation);
            System.IO.File.Delete(path);
        }

        private string CombinePaths(File file)
        {
            return CombinePaths(this.Path, file.Location);
        }
        private string CombinePaths(ItemLocation location)
        {
            return Environment.ExpandEnvironmentVariables(CombinePaths(this.Path, location));
        }

        private static string CombinePaths(string path, ItemLocation location)
        {
            return path + string.Join(IOPath.DirectorySeparatorChar.ToString(),
                location.Folders.Where(str => !string.IsNullOrEmpty(str))) + location.ItemName;
        }

        File IFileSystemService.SaveFile(File toSave)
        {
            string path = CombinePaths(toSave.Location);
            System.IO.File.WriteAllBytes(path, toSave.GetContent());
            return new LocalFile(toSave.Location, path);
        }

        public bool ContainsFile(ItemLocation potentialFile)
        {
            string path = CombinePaths(potentialFile);
            return System.IO.File.Exists(path);
        }

        public IEnumerable<File> ListFiles(ItemLocation itemLocation)
        {
            string path = CombinePaths(itemLocation);
            return from fullFilePath
                   in System.IO.Directory.EnumerateFiles(path)
                   let name = System.IO.Path.GetFileName(fullFilePath)
                   select new LocalFile(itemLocation.SetName(name), fullFilePath);

        }

        public IEnumerable<ItemLocation> GetFolders(ItemLocation root)
        {
            string path = CombinePaths(root);
            return from fullFilePath
                  in System.IO.Directory.EnumerateDirectories(path)
                   let folder = System.IO.Path.GetDirectoryName(fullFilePath)
                   select root.AddFolder(folder);
        }

        public void CreateFolder(ItemLocation folder)
        {
            string path = CombinePaths(folder);
            var toCheck = System.IO.Directory.CreateDirectory(path);

        }

        public void RemoveFolder(ItemLocation folderToDelete)
        {
            string path = CombinePaths(folderToDelete);
            //TODO: Confirm that this is correctly deleting the specified folder.
            if (Debugger.IsAttached)
                Debugger.Break();
            System.IO.Directory.Delete(path, true);
        }
    }
}
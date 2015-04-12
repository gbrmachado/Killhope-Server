using Killhope.Plugins.Manager.Domain.Release;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Killhope.Plugins.Manager.Domain.Release.DTO;

namespace Killhope.Plugins.Manager.Domain.Test.Mock
{
    public class MockLocaReleaseManager : IFileSystemService
    {

        public List<string> folders;
        public string Domain;
        public File lastFileSaved;
        public ItemLocation lastFileRemoved;
        public ReleaseDTO release { get; set; }

        public int DeletionsPerformed { get; set; }


        public IEnumerable<string> GetAvailableFolders(string path)
        {
            FolderQueries++;
            return folders;
        }

        public int DomainQueries { get; set; }
        public int FolderQueries { get; set; }


        public ReleaseDTO GetRelease(int releaseNumber)
        {
            throw new NotSupportedException();
        }




        public void SaveFile(File toSave)
        {
            lastFileSaved = toSave;
            FileSavesPerformed++;
        }

        public int FileSavesPerformed { get; set; }


        public ReleaseDTO GetRelease()
        {
            if (this.release == null)
                return new ReleaseDTO(new ReleaseManifestDTO(), new List<File>());

            return this.release;
        }

        public File GetFile(ItemLocation fileLocation)
        {
            return new MockFile(fileLocation, "");
        }

        public void RemoveFile(ItemLocation fileLocation)
        {
            this.lastFileRemoved = fileLocation;
            this.DeletionsPerformed++;
        }

        public File CreateFile(int releaseNumber, File file)
        {
            throw new NotSupportedException();
        }

        public void DeleteRelease(int releaseNumber)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<ItemLocation> GetFolders(ItemLocation root)
        {
            throw new NotSupportedException();
        }

        public bool ContainsFile(ItemLocation potentialFile)
        {
            throw new NotSupportedException();
        }

        public void CreateFolder(ItemLocation folder)
        {
            throw new NotSupportedException();
        }

        public void RemoveFolder(ItemLocation folderToDelete)
        {
            throw new NotSupportedException();
        }

        File IFileSystemService.SaveFile(File toSave)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<File> ListFiles(ItemLocation itemLocation)
        {
            throw new NotSupportedException();
        }
    }
}
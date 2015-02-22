using Killhope.Plugins.Manager.Domain.Release.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.FTP;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// Responsible for performing the upload of a release to an external server via FTP.
    /// </summary>
    public class ReleaseFTPUploader
    {
        private readonly IFTPClient uploader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpClient">The uploader to use for the transfer. Set to uload to a specific location.</param>
        /// <remarks></remarks>
        public ReleaseFTPUploader(IFTPClient ftpClient)
        {
            if (ftpClient == null)
                throw new ArgumentNullException(nameof(ftpClient));

            this.uploader = ftpClient;
        }

        public void Upload(IEnumerable<File> release, ReleaseManifestDTO.Status statusOfRelease)
        {
            if (release == null)
                throw new ArgumentNullException(nameof(release));

            if (statusOfRelease == ReleaseManifestDTO.Status.Invalid)
                throw new ArgumentException($"Invalid Status: {statusOfRelease}");

            ReleaseManifestDTO manifest = generateNewManifest(release, statusOfRelease);

            var toUpload = new ReleaseDTO(manifest, release);

            Upload(toUpload);
        }

        private string getFTPFullPath(string currentDirectory, ItemLocation restOfPath)
        {
            return currentDirectory + restOfPath.ToPath();
        }

        private void Upload(ReleaseDTO release)
        {
            //TODO: Error Handling.
            throw new NotImplementedException("Untested, and the manifest is not yet uploaded");
            //Initially, create the folder.

            string folderName = release.Manifest.Version.ToString();

            //I am not happy with the API used by the library.
            //If folderName contains a / then it will be parsed as a full path, otherwise as a relative path.
            uploader.FtpCreateDirectory(folderName);

            //Add the manifest to the folder.

            //Create the folder structure
            foreach (var folder in release.GetFolderNames())
                uploader.FtpCreateDirectory(getFTPFullPath(this.uploader.CurrentDirectory, folder));

            //Upload files
            foreach (var file in release.Files)
                uploader.Upload(new MemoryStream(file.GetContent()), getFTPFullPath(this.uploader.CurrentDirectory, file.Location));

        }

        /// <summary>
        /// Returns the integer of the next version of the 
        /// </summary>
        /// <returns></returns>
        private int getNextVersionNumber()
        {

            IEnumerable<int> validReleases = FTPUtilities.GetAvailableReleases(uploader);

            return validReleases.Max() + 1;
        }

        private string getFTPServer()
        {
            return uploader.Hostname;
        }

        private string getFTPPath()
        {
            return uploader.CurrentDirectory;
        }

        private ReleaseManifestDTO generateNewManifest(IEnumerable<File> release, ReleaseManifestDTO.Status statusOfRelease)
        {
            //creating the release requires the latest version ID.

            //The FTPclient will be pointing to the remote server.
            //At some point in the future, we will want a per-domain manifest to obtain this infomration quickly.

            ReleaseManifestDTO toReturn = new ReleaseManifestDTO();

            toReturn.NumberOfFiles = release.Count();
            toReturn.ReleaseStatus = statusOfRelease;

            //TODO: Determine the Hash
            //toReturn.Hash;
            toReturn.Version = this.getNextVersionNumber();



            return toReturn;


        }



    }
}

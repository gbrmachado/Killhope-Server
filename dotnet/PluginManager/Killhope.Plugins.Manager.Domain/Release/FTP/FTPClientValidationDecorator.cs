using Killhope.Plugins.Manager.Domain.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.FTP;

namespace Killhope.Plugins.Manager.Domain.Release.FTP
{
    public class FTPClientValidationDecorator : IFTPClient
    {
        private bool isValidSite = false;
        private readonly IFTPClient decorated;

        public FTPClientValidationDecorator(IFTPClient decorated)
        {
            if (decorated == null)
                throw new ArgumentNullException(nameof(decorated));

            this.decorated = decorated;
        }

        //Properties should be delegated directly to the inner class (it's a shame C# doesn't have better syntax for this).
        public bool EnableSSL { get { return decorated.EnableSSL; } set { decorated.EnableSSL = value; } }
        public string Hostname { get { return decorated.Hostname; } set { decorated.Hostname = value; } }
        public string Password { get { return decorated.Password; } set { decorated.Password = value; } }


        //The CurrentDirectory should be set to the FTP Root folder at initialiation. This assumption needs to be verified.
        public string CurrentDirectory { get { return decorated.CurrentDirectory; } set { validateSite(); decorated.CurrentDirectory = value; } }

        /// <summary>Determines if the underlying implementation is valid, and if not, throws an exception.</summary>
        private void validateSite()
        {
            if (isValidSite)
                return;
            performValidation();
            isValidSite = true;
        }

        private static bool hasMoreThanOne<T>(IEnumerable<T> enumerable)
        {
            bool foundOne = false;
            foreach (T a in enumerable)
            {
                if (!foundOne)
                    foundOne = true;
                else
                    return true;
            }
            return false;
        }

        private void performValidation()
        {
            //NB to maintainers: remember to call the base implementation as any calls to the current class will obviously infinitely recurse.
            string fileToFind = FTPSiteIndicator.FileName;
            bool found = false;

            //Firstly, determine if the current directory exists, if this is not the case, then throw an error.
            //Using the current library, the easiest way to do this is to perform a "List" and see if 
            var allFiles = decorated.ListDirectory();

            if (!allFiles.Any() || (!hasMoreThanOne(allFiles) && String.IsNullOrWhiteSpace(allFiles.First())))
                throw new InvalidFTPSiteException($"The supplied directory: {CurrentDirectory} on {Hostname} is not a folder.");


            foreach (var f in allFiles)
            {
                if (f == fileToFind)
                {
                    found = true;
                    break;
                }
            }

            if(!found)
                throw InvalidFTPSiteException.FileNotFound(FTPSiteIndicator.FileName, CurrentDirectory, Hostname);


            string toConvert = decorated.DownloadFile(CurrentDirectory + fileToFind).ConvertToString(resetBeforeWriting:true);
            FTPSiteIndicator file = FTPSiteIndicator.FromString(toConvert);

            if(file == null)
                throw new InvalidFTPSiteException($"File: {fileToFind} found, but no content was detected.");

            var validation = file.Validate();
            if (!validation.isValid)
                throw new InvalidFTPSiteException(validation.ToString());


        }

        //All methods below are implementations, that when called should check to confirm the current FTP is a 
        public MemoryStream DownloadFile(string sourceFilename)
        {
            validateSite();
            return decorated.DownloadFile(sourceFilename);
        }

        public bool FtpCreateDirectory(string folderName)
        {
            validateSite();
            return decorated.FtpCreateDirectory(folderName);
        }

        public List<string> ListDirectory(string directory = "")
        {
            validateSite();
            return decorated.ListDirectory(directory);
        }

        public FTPdirectory ListDirectoryDetail(string directory = "", bool doDateTimeStamp = false)
        {
            validateSite();
            return decorated.ListDirectoryDetail(directory, doDateTimeStamp);
        }

        public bool Upload(Stream memoryStream, string targetFilename)
        {
            validateSite();
            return decorated.Upload(memoryStream, targetFilename);
        }

        public class InvalidFTPSiteException : InvalidOperationException
        {
            public InvalidFTPSiteException() { }

            public InvalidFTPSiteException(string message) : base(message) { }

            //TODO: Might want to set some parameters.
            internal static Exception FileNotFound(string fileName, string currentDirectory, string hostName) => new InvalidFTPSiteException($"Could not find file: {fileName} in directory {currentDirectory} on server: {hostName}");

        }

    }
}

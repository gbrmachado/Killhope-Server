﻿using Killhope.Plugins.Manager.Domain.Release.FTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilities.FTP;

namespace Killhope.Plugins.Manager.Domain.Test
{
    internal class MockFTPClient : IFTPClient
    {
        public MockFTPClient() 
        {
        }

        public static readonly string ValidFileDownload = "HELLOWORLD";
        private bool isValid;
        private bool invalidateDownload;

        public string CurrentDirectory { get; set; }

        public bool EnableSSL { get; set; }

        public string Hostname { get; set; }

        public string Password { get; set; }


        public bool ValidationWasAttempted { get; private set; }

        public bool FolderIsCreated { get; set; }

        public int TimesValidationFileDownloaded { get; internal set; }

        public int TotalDownloadCalls { get; set; }

        public MemoryStream DownloadFile(string sourceFilename)
        {
            TotalDownloadCalls++;
            if (!isValid)
                return null;

            if (invalidateDownload)
                return new MemoryStream(Encoding.UTF8.GetBytes("NOT JSON"));

            if (sourceFilename == FTPSiteIndicator.FileName)
            {
                ValidationWasAttempted = true;
                TimesValidationFileDownloaded++;
                return new MemoryStream(Encoding.UTF8.GetBytes(new FTPSiteIndicator().ToJSONString()));
            }

            if (sourceFilename == MockFTPClient.ValidFileDownload)
                return new MemoryStream(Encoding.UTF8.GetBytes(new FTPSiteIndicator().ToJSONString()));
            throw new NotImplementedException();
        }

        internal void setAsValid()
        {
            this.isValid = true;
        }

        public bool FtpCreateDirectory(string folderName)
        {
            throw new NotImplementedException();
        }

        public List<string> ListDirectory(string directory = "")
        {
            if (!isValid)
                return new List<string> { };


            //if we are "valid", there are 2 choices, depending on: FolderIsCreated
            //Firstly: if the value is false, we only want to return "".
            //Otherwise, we want to return the values: ".", ".." and the expected files.
            if(!FolderIsCreated)
                return new List<string> { "" };

            return new List<string> { ".", "..", FTPSiteIndicator.FileName };
        }

        internal void InvalidateDownload()
        {
            this.invalidateDownload = true;
        }

        public FTPdirectory ListDirectoryDetail(string directory = "", bool doDateTimeStamp = false)
        {
            throw new NotImplementedException();
        }

        public bool Upload(Stream memoryStream, string targetFilename)
        {
            throw new NotImplementedException();
        }
    }
}
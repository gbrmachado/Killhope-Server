using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// A collection of metadata pertaining to a single release.
    /// </summary>
    public class ReleaseManifestDTO
    {

        public static readonly string FileName = "releaseInfo.json";

        public int NumberOfFiles { get; set; }

        /// <summary>The status of the release.</summary>
        /// <remarks>The status should be consistent with the data stored in the <see cref="FTPSiteIndicator"/></remarks>
        public Status ReleaseStatus { get; set; }

        /// <summary>
        /// The positive integer specifying the version number.
        /// </summary>
        /// <remarks></remarks>
        public int Version { get; set; }

        /// <summary>
        /// The hash overall for the release.
        /// </summary>
        /// <remarks>Currently unused.</remarks>
        public string Hash { get; set; }
        //TODO:
        public DateTime Date { get; set; }

        public enum Status
        {
            Invalid = 0,
            Staging = 1,
            Released = 2,
            Retired = 3
        }

        public static ReleaseManifestDTO FromByteArray(byte[] memoryStream)
        {
            return FromStream(new MemoryStream(memoryStream));
        }

        public static ReleaseManifestDTO FromStream(MemoryStream memoryStream)
        {
            string JSON = new StreamReader(memoryStream).ReadToEnd();
            return FromString(JSON);
        }

        public static ReleaseManifestDTO FromString(string JSON)
        {
            return JsonConvert.DeserializeObject<ReleaseManifestDTO>(JSON);
        }

        public byte[] ToByteArray()
        {
            string JSON = JsonConvert.SerializeObject(this);
            return Encoding.Unicode.GetBytes(JSON);
        }
    }
}

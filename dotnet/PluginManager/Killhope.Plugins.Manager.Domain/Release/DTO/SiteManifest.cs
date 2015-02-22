using Killhope.Plugins.Rocks.Domain.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Release.DTO
{
    /// <summary>
    /// Encapsulates a local file containing information about a remote server.
    /// </summary>
    public class SiteManifest
    {
        /// <summary>
        /// The path from the root of the FTP server where the releases lie.
        /// </summary>
        public string FTPPath { get; set; }

        /// <summary>
        /// The server used for the release
        /// </summary>
        public string FTPServer { get; set; }

        public string DisplayName { get; set; }

        public bool UseSFTP { get; set; }

        public string DefaultUserName { get; set; }

        public static SiteManifest FromByteStream(byte[] fileContent)
        {
            string json = Encoding.Unicode.GetString(fileContent);

            return JsonConvert.DeserializeObject<SiteManifest>(json);
        }

        internal ValidationResult Validate()
        {
            //TODO: Validation.
            return new ValidationResult();
        }

        public byte[] ToByteArray()
        {
            string JSON = JsonConvert.SerializeObject(this);
            return Encoding.Unicode.GetBytes(JSON);
        }
    }
}

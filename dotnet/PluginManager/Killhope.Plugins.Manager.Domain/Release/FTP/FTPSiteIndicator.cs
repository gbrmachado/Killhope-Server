using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace Killhope.Plugins.Manager.Domain.Release.FTP
{
    /// <summary>DTO of a file specifying metadata associated with the top level of an external FTP Site.</summary>
    /// <remarks>This file should be located in the root of the remote FTP directory and shuld be well-formed, if this is not the case then FTP operations should not be allowed to continue.</remarks>
    public class FTPSiteIndicator
    {
        /// <summary>The name that this file will be called on the server.</summary>
        public static readonly string FileName = "site_info.json";

        public int CurrentStableRelease { get; set; }

        public int? CurrentStagingRelease { get; set; }

        /// <summary>Additional metadata to allow the file to be forwards-compatible.</summary>
        [JsonExtensionData]
        private Dictionary<string, JToken> extra { get; set; }

        public static FTPSiteIndicator FromString(string JSON) => JsonConvert.DeserializeObject<FTPSiteIndicator>(JSON);

        public static string ToJSONString(FTPSiteIndicator siteData) => siteData?.ToJSONString();

        public string ToJSONString() => JsonConvert.SerializeObject(this);
    }
}

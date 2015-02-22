using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Release.Local_IO
{
    /// <summary>
    /// Allows OS-independent operations to be performed on local files from an FTP host.
    /// </summary>
    public static class LocalFTPServiceUtilities
    {
        /// <summary>
        /// Transforms a root folder path and the name of a site into a valid fully qualified path for the site.
        /// </summary>
        /// <param name="pathOfRoot">The path to the folder where each individual FTP server's data is saved.</param>
        /// <param name="siteName">The name of the site</param>
        /// <returns></returns>
       public static string GetFolderPathForSite(string pathOfRoot, string siteName, bool failIfNotExists = true)
        {
            if (String.IsNullOrEmpty(siteName))
                throw new ArgumentException($"{siteName} is an invalid value for the name of the site");

            if (!Directory.Exists(pathOfRoot))
                throw new ArgumentException($"The supplied root: {pathOfRoot} does not exist.");

            string invalid = new string(Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).Distinct().ToArray());

            foreach (char c in invalid)
                siteName = siteName.Replace(c.ToString(), "_");
            

            string fullpath = Path.Combine(pathOfRoot, siteName);

            if (failIfNotExists && !Directory.Exists(fullpath))
                throw new ArgumentException($"The obtained path: {fullpath} does not exist.");

            return fullpath;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Utilities.FTP;

namespace Killhope.Plugins.Manager.Domain.Release
{
    public static class FTPUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploader">A FTPclient set with the root as the upload folder for the site in question.</param>
        /// <returns>The available releases from the external site.</returns>
        public static IEnumerable<int> GetAvailableReleases(this IFTPClient uploader)
        {
            int DO_NOT_USE = -1;
            return from a in uploader.ListDirectory()
            where int.TryParse(a, out DO_NOT_USE)
            select DO_NOT_USE;
        }
    }
}

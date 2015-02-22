using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Release.Local_IO
{
    /// <summary>
    /// Allows OS-independent operations to be performed on local files from an FTP host.
    /// </summary>
    public interface ILocalFTPService
    {
        /// <summary>
        /// Transforms a root folder path and the name of a site into a valid fully qualified path for the site.
        /// </summary>
        /// <param name="pathOfRoot"></param>
        /// <param name="siteName"></param>
        /// <returns></returns>
        string getFolderPathForSite(string pathOfRoot, string siteName);
    }
}

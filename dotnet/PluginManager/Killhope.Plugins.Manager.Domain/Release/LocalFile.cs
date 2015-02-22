using System;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// A file that exists locally on the disk.
    /// </summary>
    public class LocalFile : File
    {
        /*
         * TODO:
         * An ItemLocation is a RELATIVE path, the fullPath contains the relative location.
         * There may be a better way to handle this than to save the same data in the Path and the location.
         * 
         */

        public string Path { get; private set; }

        public LocalFile(ItemLocation location, string fullPath) : base(location)
        {
            if (fullPath == null)
                throw new ArgumentNullException("path");

            Path = fullPath;
        }

        public override byte[] GetContent()
        {
            return System.IO.File.ReadAllBytes(Path);
        }
    }
}

using Killhope.Plugins.Manager.Domain.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// Once side of a release (a collection of files, associated hashes and server metadata).
    /// </summary>
    public abstract class ReleaseSide
    {

        public abstract bool CanAdd { get; }
        public abstract bool CanRemove { get; }
        public abstract bool CanRefresh { get; }

        private SortedSet<File> _files;

        public int FileCount { get { return ContainedFiles.Count(); } }

        public abstract void Add(File file);
        public abstract void Remove(ItemLocation fileLocation);
        public abstract void InitialLoad();
        public abstract void Refresh();

        /// <summary>
        /// Returns a list of files ordered in some order that will allow for zipping of the collection in the future.
        /// </summary>
        public IEnumerable<File> ContainedFiles
        {
            get
            {
                //Lazy loaded.
                if (this._files == null)
                    Refresh();
                return this._files;
            }
            protected set
            {
                _files = new SortedSet<File>(value);
            }
        }

        protected bool AddFile(File f)
        {
            if (this._files == null)
                Refresh();
            return _files.Add(f);
        }

        public IEnumerable<string> GetHashes(IHashProvider DefaultHashProvider)
        {
            //TODO: This class should handle caching of the data.
            //._content is used for performance reasons (as we do not need to copy the byte array).
            return from file in ContainedFiles select DefaultHashProvider.ComputeHash(file.GetContent());
        }

        public File GetFileAtLocation(ItemLocation location)
        {
            return ContainedFiles.Where(f => f.Location.Equals(location)).FirstOrDefault();
        }

        protected void RemoveFile(ItemLocation fileLocation)
        {
            if (this._files == null)
                Refresh();
            _files.RemoveWhere(file => file.Location.Equals(fileLocation));
        }
    }
}

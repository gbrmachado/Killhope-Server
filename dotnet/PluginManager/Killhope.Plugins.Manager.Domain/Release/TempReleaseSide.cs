using Killhope.Plugins.Manager.Domain.Release.Local_IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Release
{
    /// <summary>
    /// A side of a release locally on the disk which may be modifed.
    /// </summary>
    public class TempReleaseSide : ReleaseSide
    {
        private readonly LocalReleaseManager releaseManager;

        public TempReleaseSide(LocalReleaseManager releaseManager)
        {
            if (releaseManager == null)
                throw new ArgumentNullException("releaseManager");

            this.releaseManager = releaseManager;
        }

        public override bool CanAdd { get { return true; } }
        public override bool CanRemove { get { return true; } }
        public override bool CanRefresh { get { return true; } }

        public override void Add(File file)
        {
            if (!this.AddFile(file))
                throw new ArgumentException("file failed to be added");

            try
            {
                this.releaseManager.AddFileToRelease(file);
            }
            catch (Exception)
            {
                this.Remove(file.Location);
                throw;
            }
        }

        public override void Remove(ItemLocation fileLocation)
        {
            base.RemoveFile(fileLocation);

            try
            {
                this.releaseManager.RemoveFileFromRelease(fileLocation);
            }
            catch (Exception) { }
        }

        public override void InitialLoad()
        {
            Refresh();
        }

        public override void Refresh()
        {
            base.ContainedFiles = this.releaseManager.GetRelease().Files;
        }

    }
}

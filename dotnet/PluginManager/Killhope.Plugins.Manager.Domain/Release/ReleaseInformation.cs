using Killhope.Plugins.Manager.Domain.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killhope.Plugins.Manager.Domain.Release;

namespace Killhope.Plugins.Manager.Domain
{
    /// <summary>
    /// An abstraction over two releases, one commited to the system, and one pending.
    /// </summary>
    public class ReleaseInformation
    {

        private readonly ReleaseSide Left;
        private readonly ReleaseSide Right;

        private bool? hasChanges;

        public IHashProvider DefaultHashProvider { get; set; }

        public IEnumerable<File> LeftFiles { get { return Left.ContainedFiles; } }
        public IEnumerable<File> RightFiles { get { return Right.ContainedFiles; } }

        public ReleaseInformation(ReleaseSide left, ReleaseSide right) 
        {
            Left = left;
            Right = right;
        }

        private void tryRefresh(ReleaseSide side)
        {
            try
            {
                if (side.CanRefresh)
                    side.Refresh();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Refreshes both sides of the relese.
        /// </summary>
        public void Refresh()
        {
            tryRefresh(Left);
            tryRefresh(Right);
            hasChanges = null;
        }

        /// <summary>
        /// Whether there are differences in the files betwen the left and the right side of the Release.
        /// </summary>
        /// <returns></returns>
        public bool HasChanges()
        {
            if (hasChanges.HasValue)
                hasChanges = findChanges();

            return hasChanges.Value;
        }

        private bool? findChanges()
        {
            //The most efficient way will be to initially check the number of files on both sides.

            if (Left.FileCount != Right.FileCount)
                return true;


            //After this, check the file locations to ensure that they match.

            bool hasNoLocationChanges = Left.GetHashes(DefaultHashProvider).Zip(Right.GetHashes(DefaultHashProvider),
    (leftHash, rightHash) => leftHash == rightHash).All(Bool => Bool);

            if (!hasNoLocationChanges)
                return true;

            //If we can't determine the hashes, then presume the files are the same.
            if (DefaultHashProvider == null)
                return false;

            //Finally, check the hashes of each of the files.

            bool hasNoHashChanges = Left.GetHashes(DefaultHashProvider).Zip(Right.GetHashes(DefaultHashProvider),
    (leftHash, rightHash) => leftHash == rightHash).All(Bool => Bool);


            return !hasNoHashChanges;
        }



        /// <summary>
        /// Performs a compare operation using the specified FileComparer with the two files at the specified location.
        /// </summary>
        /// <param name="location"></param>
        public void Compare(ItemLocation location, IFileComparer fileComparer)
        {
            File left = Left.GetFileAtLocation(location);
            File right = Right.GetFileAtLocation(location);

            //TODO: Show a message saying that one of the files does not exist.
            if (left == null || right == null)
                return;

            try
            {
                fileComparer.Compare(left, right);
            }
            catch (Exception) { }
        }
    }
}

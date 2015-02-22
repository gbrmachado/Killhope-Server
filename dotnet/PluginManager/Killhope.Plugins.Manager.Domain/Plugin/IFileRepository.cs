namespace Killhope.Plugins.Manager.Domain
{

    /// <summary>
    /// Allows saving and loading of files from storage media.
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Creates the file at the specified location. If it already exists then overwrite the previous data.
        /// </summary>
        /// <param name="file">The specified file contents to be updated.</param>
        void Save(File file);
        File Load(ItemLocation location);

    }
}

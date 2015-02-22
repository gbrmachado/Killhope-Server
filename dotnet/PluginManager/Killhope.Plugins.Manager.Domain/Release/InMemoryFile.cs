using System.IO;

namespace Killhope.Plugins.Manager.Domain.Release
{
    internal class InMemoryFile : File
    {
        private MemoryStream memoryStream;

        public InMemoryFile(ItemLocation itemLocation, byte[] data) : base(itemLocation)
        {
            this.memoryStream = new MemoryStream(data);
        }

        public InMemoryFile(ItemLocation itemLocation, MemoryStream memoryStream) : base(itemLocation)
        {
            this.memoryStream = memoryStream;
        }

        public override byte[] GetContent()
        {
            return memoryStream.ToArray();
        }
    }
}
using System.IO;

namespace Killhope.Plugins.Manager.Domain.Application
{
    public static class StreamExtensions
    {
        public static string ConvertToString(this Stream stream) => new StreamReader(stream).ReadToEnd();
    }
}

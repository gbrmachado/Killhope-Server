using System.IO;

namespace Killhope.Plugins.Manager.Domain.Application
{
    public static class StreamExtensions
    {
        public static string ConvertToString(this Stream stream, bool resetBeforeWriting = false)
        {
            //Might be useful to check if we can seek here, but an exception will be thrown regardless.
            if (resetBeforeWriting)
                stream.Position = 0;

            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}

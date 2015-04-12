using Killhope.Plugins.Rocks.Domain.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Presentation
{
    public class ExistingJSONFileHandler : IJSONModificationService
    {
        private string path;

        public ExistingJSONFileHandler(string path)
        {
            //create a fileInfo to determine if the file exists.
            new FileInfo(path);
            this.path = path;
        }

        public string Load()
        {
            return File.ReadAllText(path);
        }

        public void Save(string JSON)
        {
            File.WriteAllText(path, JSON);
        }
    }
}

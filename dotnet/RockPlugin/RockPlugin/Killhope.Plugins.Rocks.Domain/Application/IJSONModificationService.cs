using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Application
{
    public interface IJSONModificationService
    {
        string Load();
        void Save(string JSON);
    }
}

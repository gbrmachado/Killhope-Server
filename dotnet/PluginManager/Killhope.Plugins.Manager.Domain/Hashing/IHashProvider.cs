using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Hashing
{
    public interface IHashProvider
    {
        string ComputeHash(byte[] data);
    }
}

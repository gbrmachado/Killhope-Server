using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Manager.Domain.Release
{
    public interface IFileComparer
    {
        void Compare(File left, File right);
    }
}

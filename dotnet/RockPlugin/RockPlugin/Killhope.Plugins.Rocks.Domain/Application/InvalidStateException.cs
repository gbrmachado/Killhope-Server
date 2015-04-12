using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Application
{
    /// <summary>
    /// Specifies that the object is in an invalid state.
    /// </summary>
    public class InvalidStateException : InvalidOperationException
    {
        //TODO: CTOR

        public InvalidStateException() : base() { }
        public InvalidStateException(string message) : base(message) { }


    }
}

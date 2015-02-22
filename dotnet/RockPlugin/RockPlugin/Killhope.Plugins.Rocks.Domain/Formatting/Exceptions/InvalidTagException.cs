using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting
{
    public class InvalidFormatException : ArgumentException
    {
        public InvalidFormatException() : base() { }
        public InvalidFormatException(string message) : base(message) { }
    }
}

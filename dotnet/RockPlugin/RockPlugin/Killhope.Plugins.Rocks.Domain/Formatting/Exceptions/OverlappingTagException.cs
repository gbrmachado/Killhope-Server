using Killhope.Plugins.Rocks.Domain.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting.Exceptions
{
    public class OverlappingTagException : InvalidFormatException
    {
        public OverlappingTagException(string attemptedToParse, string tagName, int startPosition, int secondStart, int endPosition)
        {
            if(tagName == null)
                tagName = "";

            //TODO: Impl
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting
{
    class FormatFactory
    {
        internal static readonly HTMLDualTagFormatting u = new HTMLDualTagFormatting("u");
        internal static readonly HTMLDualTagFormatting b = new HTMLDualTagFormatting("b");
        internal static readonly HTMLDualTagFormatting i = new HTMLDualTagFormatting("i");
        internal static readonly SingleHTMLTag br = new SingleHTMLTag("br");

        public static List<TextFormattingRule> GetInstance()
        {
            return new List<TextFormattingRule> { u, b, i, br };
        }
    }
}

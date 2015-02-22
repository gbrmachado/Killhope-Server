using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Application
{
    public interface IRockListView
    {
        List<Content> Content { get; set; }
        string Formula { get; set; }
        List<int> GalleryImages { get; set; }
        List<string> Images { get; set; }
        string Title { get; set; }
        string UniqueId { get; set; }
        string VideoPath { get; set; }
        bool CanSave { get; }
        void LoadRock(Rock selected);
    }
}

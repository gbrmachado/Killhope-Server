using Killhope.Plugins.Rocks.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Test.Mocks
{
    class MockRockListView : IRockListView
    {

        private Rock r = new Rock();

        public List<Content> Content { get { return r.Content; } set { r.Content = value; } }
        public string Formula { get { return r.Formula; } set { r.Formula = value; } }
        public List<int> GalleryImages { get { return r.GalleryImages; } set { r.GalleryImages = value; } }
        public List<string> Images { get { return r.Images; } set { r.Images = value; } }
        public string Title { get { return r.Title; } set { r.Title = value; } }
        public string UniqueId { get { return r.UniqueId; } set { r.UniqueId = value; } }
        public string VideoPath { get { return r.VideoPath; } set { r.VideoPath = value; } }

        //if we haven't changed, we can't save.
        public bool CanSave { get { return HasChanged; } }

        public void LoadRock(Rock selected)
        {
            
        }

        internal void MakeValidRock(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception(String.Format("Invalid ID: {0}", id));

            this.UniqueId = id;
            this.Images = new List<string>();
            this.GalleryImages = new List<int>();
            this.Title = "Test-Rock";
            this.Formula = "";
            this.Content = new List<Content>();
            this.Content.Add(new Content() { Data = "", Title = "Test-Content" });
            this.HasChanged = true;
        }
    
        public  bool HasChanged { get; set; }}
}

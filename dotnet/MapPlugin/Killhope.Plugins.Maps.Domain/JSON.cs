using System.Collections.Generic;

namespace Killhope.Plugins.Maps.Domain
{
    public class JSON
    {
        public class Exhibition
        {
            public string id { get; set; }
            public float x { get; set; }
            public float y { get; set; }
        }

        public class Point
        {
            public Point(float x, float y) { this.x = x; this.y = y; }

            public float x { get; set; }
            public float y { get; set; }
        }

        public class Room
        {
            public string id { get; set; }
            public List<string> rocks { get; set; }
            public List<Point> points { get; set; }
        }

        public class Map
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public List<Exhibition> Exhibitions { get; set; }
            public List<Room> Rooms { get; set; }
        }
    }
}

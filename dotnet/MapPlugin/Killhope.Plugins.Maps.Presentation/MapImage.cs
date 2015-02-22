using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

using Room = Killhope.Plugins.Maps.Domain.JSON.Room;
using Exhibition = Killhope.Plugins.Maps.Domain.JSON.Exhibition;


namespace Killhope.Plugins.Maps.Presentation
{
    /// <summary>
    /// Used to encapsulate a map and generate the 
    /// </summary>
    public class MapImage
    {
        private readonly List<Room> rooms;
        private readonly List<Exhibition> exhibitions;
        private Image map;


        public delegate void ImageChangedEventHandler(object sender, EventArgs e);
        public event ImageChangedEventHandler ImageChanged;

        //TODO: Block event being fired fo multiple changes and raise afterwards.

        public MapImage(Image initialMap)
        {
            if (initialMap == null)
                throw new ArgumentNullException(nameof(initialMap));
            rooms = new List<Room>();
            exhibitions = new List<Exhibition>();
            map = initialMap;
        }



        public Image Map
        {
            get { return map; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Map));
                map = value;
                OnMapChanged();
            }
        }

        public void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            rooms.Add(room);
            OnMapChanged();
        }

        public void DeleteRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            rooms.Remove(room);
            OnMapChanged();
        }

        public void AddExhibition(Exhibition exhibition)
        {
            if (exhibition == null)
                throw new ArgumentNullException(nameof(exhibition));
            exhibitions.Add(exhibition);
            OnMapChanged();
        }

        public void DeleteExhibition(Exhibition exhibition)
        {
            if (exhibition == null)
                throw new ArgumentNullException(nameof(exhibition));
            exhibitions.Remove(exhibition);
            OnMapChanged();
        }


        public virtual void Draw(Graphics g)
        {
            g.DrawImage(this.map, new Point(0, 0));

            g.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var room in this.rooms)
                DrawRoom(g, room);
            foreach (var exhibition in this.exhibitions)
                DrawExhibition(g, exhibition, getExhibitionBorderPen(), getExhibitionBrush());

        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnMapChanged(EventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, e);
        }

        protected virtual void OnMapChanged()
        {
            OnMapChanged(EventArgs.Empty);
        }

        protected virtual void DrawRoom(Graphics g, Room room)
        {
            if (g == null)
                throw new ArgumentNullException(nameof(g));
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            var points = getPoints(room);
            g.DrawPolygon(getRoomPen(), points);
            g.FillPolygon(getRoomBrush(), points);
        }

        private Point[] getPoints(Room room) => (from point in room.points select new Point((int)point.x, (int)point.y)).ToArray();


        protected virtual Pen getRoomPen() => new Pen(Color.Black);
        protected virtual Brush getRoomBrush() => new SolidBrush(Color.Red);

        protected void DrawExhibition(Graphics g, Exhibition exhibition)
        {
            DrawExhibition(g, exhibition, getExhibitionBorderPen(), getExhibitionBrush());
        }

        protected virtual void DrawExhibition(Graphics g, Exhibition exhibition, Pen pen, Brush brush)
        {
            if (g == null)
                throw new ArgumentNullException(nameof(g));
            if (exhibition == null)
                throw new ArgumentNullException(nameof(exhibition));
            if (pen == null)
                throw new ArgumentNullException(nameof(pen));
            if (brush == null)
                throw new ArgumentNullException(nameof(brush));
            
            //An exhibition is the center, whereas GDI takes the top left corner, radius is modified to handle this.
            var radius = getExhibitionRadius();
            var halfRad = radius / 2;
            DrawCircle(g, pen, brush, exhibition.x - halfRad, exhibition.y - halfRad, radius);
        }

        protected virtual Pen getExhibitionBorderPen() => new Pen(Color.Black, 2.5f);

        protected virtual int getExhibitionRadius() => 20;

        protected Brush getExhibitionBrush() => new SolidBrush(Color.Red);

        protected void DrawCircle(Graphics g, Pen exteriorPen, Brush interiorBrush, float x, float y, float radius)
        {
            g.DrawEllipse(exteriorPen, x, y, radius, radius);
            g.FillEllipse(interiorBrush, x, y, radius, radius);
        }

    }
}

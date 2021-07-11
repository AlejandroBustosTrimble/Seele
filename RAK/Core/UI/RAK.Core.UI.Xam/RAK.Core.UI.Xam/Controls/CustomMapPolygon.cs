using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace RAK.Core.UI.Xam.Controls
{
    public class CustomMapPolygon : Xamarin.Forms.Maps.Map
    {
        public List<Zona> Zonas { get; set; }

        public CustomMapPolygon()
        {
            Zonas = new List<Zona>();
        }
    }

    public class Zona
    {
        public List<Position> Ubicacion { get; set; }
        public string Nombre { get; set; }
        public string ColorFondo { get; set; }
        public int Orden { get; set; }
    }
}
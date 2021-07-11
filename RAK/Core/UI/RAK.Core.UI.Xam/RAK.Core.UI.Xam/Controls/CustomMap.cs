using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace RAK.Core.UI.Xam.Controls
{
	public class CustomMap : Xamarin.Forms.Maps.Map
	{
		public List<Linea> Lineas { get; set; }

		public CustomMap()
		{
			Lineas = new List<Linea>();
		}
	}

	public class Linea
	{
		public Position Desde { get; set; }

		public Position Hasta { get; set; }

		public ColorLinea Color { get; set; }
	}

	public enum ColorLinea
	{
		Rojo,
		Amarillo,
		Verde
	}
}

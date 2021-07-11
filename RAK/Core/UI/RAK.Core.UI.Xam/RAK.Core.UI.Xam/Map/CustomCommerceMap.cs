using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace RAK.Core.UI.Xam.Map
{
    public class CustomCommerceMap : Xamarin.Forms.Maps.Map
	{
		public List<CustomPin> CustomPins { get; set; } = new List<CustomPin>();

		public CustomCommerceMap(MapSpan span) : base(span)
		{
		}

		public class CustomPin : Pin
		{
			public string Name { get; set; }

			public string Image { get; set; }

			public List<Service> Services { get; set; } = new List<Service>();
		}

		public class Service
		{
			public string Name { get; set; }

			public string Image { get; set; }

			public bool Available { get; set; }
		}
	}
}

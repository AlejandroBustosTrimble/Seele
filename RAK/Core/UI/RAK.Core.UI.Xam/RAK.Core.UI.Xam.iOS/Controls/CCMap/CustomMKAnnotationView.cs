using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using UIKit;
using static Fwk.XAM.Map.CustomCommerceMap;

namespace Fwk.XAM.iOS.Control
{
	public class CustomMKAnnotationView : MKAnnotationView
	{
		public string Name { get; set; }

		public string Address { get; set; }

		//public string Image { get; set; }
		public string CommerceImage { get; set; }

		public List<Service> Services { get; set; } = new List<Service>();

		public CustomMKAnnotationView(IMKAnnotation annotation, string id)
			: base(annotation, id)
		{
		}
	}
}
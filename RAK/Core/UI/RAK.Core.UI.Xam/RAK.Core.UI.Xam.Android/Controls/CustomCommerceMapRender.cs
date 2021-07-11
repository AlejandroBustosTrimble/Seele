using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Drawing;
using RAK.Core.UI.Xam.Map;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomCommerceMap), typeof(CustomCommerceMapRender))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public abstract class CustomCommerceMapRender : MapRenderer, GoogleMap.IInfoWindowAdapter
	{

		#region Abstracts
		/// <summary>
		/// Implementar asi: Resource.Drawable.map_marker_rs
		/// </summary>
		protected abstract int map_marker_rs { get; }

		/// <summary>
		/// Implementar asi: Resource.Layout.CustomCommerceMapInfoWindow
		/// </summary>
		protected abstract int CustomCommerceMapInfoWindow { get; }

		/// <summary>
		/// Implementar asi: Resource.Id.InfoWindowName
		/// </summary>
		protected abstract int InfoWindowName { get; }

		/// <summary>
		/// Implementar asi: Resource.Id.InfoWindowAddress
		/// </summary>
		protected abstract int InfoWindowAddress { get; }

		/// <summary>
		/// Implementar asi: Resource.Id.InfoWindowImage
		/// </summary>
		protected abstract int InfoWindowImage { get; }

		/// <summary>
		/// Implementar asi: Resource.Id.InfoWindowServices
		/// </summary>
		protected abstract int InfoWindowServices { get; }

		#endregion


		List<CustomCommerceMap.CustomPin> customPins;

		public CustomCommerceMapRender(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				NativeMap.InfoWindowClick -= OnInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomCommerceMap)e.NewElement;
				customPins = formsMap.CustomPins;
			}
		}

		protected override void OnMapReady(GoogleMap map)
		{
			base.OnMapReady(map);

			NativeMap.InfoWindowClick += OnInfoWindowClick;
			NativeMap.SetInfoWindowAdapter(this);
		}

		protected override MarkerOptions CreateMarker(Pin pin)
		{
			var marker = new MarkerOptions();
			marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
			marker.SetTitle(pin.Label);
			marker.SetSnippet(pin.Address);
			marker.SetIcon(BitmapDescriptorFactory.FromResource(map_marker_rs));
			return marker;
		}

		void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			var customPin = GetCustomPin(e.Marker);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}
		}

		public Android.Views.View GetInfoContents(Marker marker)
		{
			var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
			if (inflater != null)
			{
				Android.Views.View view;

				var customPin = GetCustomPin(marker);
				if (customPin == null)
				{
					throw new Exception("Custom pin not found");
				}

				view = inflater.Inflate(CustomCommerceMapInfoWindow, null);

				var infoName = view.FindViewById<TextView>(InfoWindowName);
				var infoAddress = view.FindViewById<TextView>(InfoWindowAddress);
				var infoImage = view.FindViewById<ImageView>(InfoWindowImage);
				var infoLayoutService = view.FindViewById<LinearLayout>(InfoWindowServices);

				infoName.Text = customPin.Name;
				infoAddress.Text = customPin.Address;
				//infoImage.SetImageURI(Android.Net.Uri.Parse(customPin.Image));

				try
				{
					var imageBitmap = this.GetRoundedBitmapFromUrl(customPin.Image);
					infoImage.SetImageDrawable(imageBitmap);

					//var imageBitmap = this.GetBitmapFromUrl(customPin.Image);
					//infoImage.SetImageBitmap(imageBitmap);
				}
				catch { }

				#region Imagenes de servicio

				foreach (var s in customPin.Services)
				{
					ImageView iv = new ImageView(this.Context);
					var ibm = this.GetBitmapFromUrl(s.Image);
					iv.SetImageBitmap(ibm);

					iv.LayoutParameters = new LinearLayout.LayoutParams(50, 50);

					infoLayoutService.AddView(iv);
				}

				#endregion

				return view;
			}
			return null;
		}

		public Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}

		CustomCommerceMap.CustomPin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
			foreach (var pin in customPins)
			{
				if (pin.Position == position)
				{
					return pin;
				}
			}
			return null;
		}

		private RoundedBitmapDrawable GetRoundedBitmapFromUrl(string url)
		{
			using (WebClient webClient = new WebClient())
			{
				byte[] bytes = webClient.DownloadData(url);
				if (bytes != null && bytes.Length > 0)
				{
					Bitmap bm = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
					RoundedBitmapDrawable roundedBitmapDrawable = RoundedBitmapDrawableFactory.Create(null, bm);
					roundedBitmapDrawable.Circular = true;
					//roundedBitmapDrawable.CornerRadius = 2;

					return roundedBitmapDrawable;
				}
			}
			return null;
		}

		private Bitmap GetBitmapFromUrl(string url)
		{
			using (WebClient webClient = new WebClient())
			{
				byte[] bytes = webClient.DownloadData(url);
				if (bytes != null && bytes.Length > 0)
				{
					return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
				}
			}
			return null;
		}
	}
}
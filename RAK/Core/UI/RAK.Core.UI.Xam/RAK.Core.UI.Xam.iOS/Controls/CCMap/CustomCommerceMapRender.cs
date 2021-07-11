using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using Foundation;
using Fwk.XAM.iOS.Control;
using Fwk.XAM.Map;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCommerceMap), typeof(CustomCommerceMapRender))]
namespace Fwk.XAM.iOS.Control
{
    public abstract class CustomCommerceMapRender : MapRenderer
    {
        protected abstract string Marker { get; }

        UIView customPinView;
        List<CustomCommerceMap.CustomPin> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomCommerceMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }


        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            //if (annotation is MKAnnotationWrapper)
            //{
            //	return null;
            //}

            //var customPin = GetCustomPin(annotation as MKPointAnnotation);
            var customPin = GetCustomPin(annotation.Coordinate);
            if (customPin == null)
            {
                //throw new Exception("Custom pin not found");
                return null;
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Name);
            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Name);
                annotationView.Image = UIImage.FromFile(Marker);

                // -- Seteamos la imagen del comercio
                try
                {
                    var image = FromUrl(customPin.Image);
                    image = image.Scale(new CGSize(50, 50));

                    var uiv = new UIImageView(image);
                    uiv.Layer.CornerRadius = image.Size.Width / 2;
                    uiv.Layer.MasksToBounds = true;

                    annotationView.LeftCalloutAccessoryView = uiv;
                }
                catch { }


                // -- Creamos stack para servicios
                UIStackView serviceStack = new UIStackView()
                {
                    Axis = UILayoutConstraintAxis.Horizontal
                };

                // -- Cargamos imagenes de servicio
                foreach (var s in customPin.Services)
                {
                    var image = FromUrl(s.Image);
                    image = image.Scale(new CGSize(25, 25));

                    var uiv = new UIImageView(image);

                    serviceStack.AddArrangedSubview(uiv);
                }

                // -- Creamos stack general
                UIStackView generalStack = new UIStackView()
                {
                    Axis = UILayoutConstraintAxis.Vertical
                };

                // -- Creamos y configuramos label de direccion
                UILabel label = new UILabel { Text = customPin.Address };
                label.Font = label.Font.WithSize(14);
                label.TextColor = UIColor.Gray;

                // -- Agregamos
                generalStack.AddArrangedSubview(label);
                generalStack.AddArrangedSubview(serviceStack);

                // -- Agregamos
                annotationView.DetailCalloutAccessoryView = generalStack;


                ((CustomMKAnnotationView)annotationView).Name = customPin.Name;
                ((CustomMKAnnotationView)annotationView).Address = customPin.Address;
                ((CustomMKAnnotationView)annotationView).CommerceImage = customPin.Image;
                //((CustomMKAnnotationView)annotationView).Image = customPin.Image;
            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            CustomMKAnnotationView customView = e.View as CustomMKAnnotationView;
            customPinView = new UIView();

            //customPinView.Frame = new CGRect(0, 0, 50, 50);
            //var image = new UIImageView(new CGRect(0, 0, 50, 50));
            //try
            //{
            //	image.Image = FromUrl(customView.CommerceImage);
            //}
            //catch { }
            //customPinView.AddSubview(image);
            //customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
            //e.View.AddSubview(customPinView);
        }

        UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            CustomMKAnnotationView customView = e.View as CustomMKAnnotationView;
            //if (!string.IsNullOrWhiteSpace(customView.Url))
            //{
            //	UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
            //}
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }


        CustomCommerceMap.CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        CustomCommerceMap.CustomPin GetCustomPin(CLLocationCoordinate2D coordinates)
        {
            var position = new Position(coordinates.Latitude, coordinates.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}
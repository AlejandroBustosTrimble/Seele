using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.Geolocator
{
    public static class Geolocator
    {
        /// <summary>
        ///  Retornamos la Geolocalizacion del usuario
        /// </summary>
        public static async Task<Geoposition> GetGeolocation()
        {
            var positionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            if (positionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.LocationWhenInUse });
                positionStatus = results[Permission.LocationWhenInUse];

                if (positionStatus != PermissionStatus.Granted)
                    return null;
            }

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            if (position != null)
            {
                var geoposition = new Geoposition()
                {
                    Lat = position.Latitude,
                    Lon = position.Longitude
                };

                return geoposition;
            }
            else return null;
        }
    }
}

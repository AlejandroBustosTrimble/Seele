using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.Controls
{
    public static class ImagePicker
    {
        private const string extension = "jpg";
        private const int maxWidthHeight = 400;

        /// <summary>
        /// Stream de una foto almacenada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static async Task<Imagen> PickPhoto()
        {
            if (CrossMedia.Current.IsTakePhotoSupported)
            {
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                if (storageStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    storageStatus = results[Permission.Storage];
                }

                var imagen = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    MaxWidthHeight = maxWidthHeight,
                    PhotoSize = PhotoSize.MaxWidthHeight
                });
                if (imagen != null)
                {
                    // -- Tomamos la extension
                    var ext = imagen.Path.Split('.').ToList().Last();
                    using (var memoryStream = new MemoryStream())
                    {
                        imagen.GetStreamWithImageRotatedForExternalStorage().CopyTo(memoryStream);
                        imagen.Dispose();

                        return new Imagen { Data = memoryStream.ToArray(), Extension = ext };
                    }


                }
            }
            return null;
        }

        /// <summary>
        /// Tomamos una foto desde la camara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static async Task<TakePhotoResponse> TakePhoto(TakePhotoRequest Req)
        {
            TakePhotoResponse Response = new TakePhotoResponse();
            Response.Result = false;

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = Req.Directory/*"Sample"*/,
                    Name = Req.Name, /*$"imagentomada.{extension}"*/
                    MaxWidthHeight = Req.maxWidthHeight, /*maxWidthHeight,*/
                    PhotoSize = Req.Size,/*PhotoSize.MaxWidthHeight*/
                });

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStreamWithImageRotatedForExternalStorage().CopyTo(memoryStream);
                        file.Dispose();

                        Response.Result = true;
                        Response.Image = new Imagen { Data = memoryStream.ToArray(), Extension = extension };
                        return Response;
                    }
                }
            }
            else
            {
                Response.ResultMessage = "No se puede llevar adelante la operacion sin los permisos requeridos.";
            }

            return Response;
        }
    }

    public class Imagen
    {
        public byte[] Data { get; set; }

        public string Extension { get; set; }
    }

    public class TakePhotoResponse
    {

        /// <summary>
        /// Resultado, si es false devuelve error en ResultMessage
        /// </summary>
        public bool Result { get; set; }
        public string ResultMessage { get; set; }
        public Imagen Image { get; set; }
    }

    public class TakePhotoRequest
    {
        public string Directory { get; set; }
        public string Name { get; set; }
        public int maxWidthHeight { get; set; }
        public PhotoSize Size { get; set; }
    }

}



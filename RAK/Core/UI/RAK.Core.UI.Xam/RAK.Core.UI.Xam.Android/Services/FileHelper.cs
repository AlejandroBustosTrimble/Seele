using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(FileHelper))]
namespace RAK.Core.UI.Xam.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        // TODO_RAK: algun momento en el fwk de xam vamos a necesitar un pro xam, otro android y un ios
        // Por ahora tenemos solo el Xam, cuando tengamos los 3 vamos a poder pasar esto a los respectivos proyectos

        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Obtiene el full path de un archivo (recurso, archivo html, etc) perteneciente al proyecto
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public string GetProjectFilePath(string relativeFilePath)
        {
            var basePath = "file:///android_asset/";

            return (basePath + relativeFilePath);
        }

        /// <summary>
        /// Obtiene el texto de un archivo de recursos
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public string ReadTextAssetFileContent(string relativeFilePath)
        {
            string content = null;
            var path = this.GetProjectFilePath(relativeFilePath);

            var fileExists = File.Exists(path);

            AssetManager assets = Forms.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(relativeFilePath)))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }

        public async Task<string> SaveFileToDownloadsFolder(byte[] bytes, String fileName)
        {
            var positionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (positionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                positionStatus = results[Permission.Storage];

                if (positionStatus != PermissionStatus.Granted)
                    return String.Empty;
            }

            var downloadDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            var filePath = Path.Combine(downloadDirectory, fileName);

            File.WriteAllBytes(filePath, bytes);


            return filePath;
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using RAK.Core.UI.Xam.iOS;

[assembly: Dependency(typeof(FileHelper))]
namespace RAK.Core.UI.Xam.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }

        public string GetProjectFilePath(string relativeFilePath)
        {
            return Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "CustomAssets"), relativeFilePath);
        }

        public string ReadTextAssetFileContent(string relativeFilePath)
        {
            var fullPath = this.GetProjectFilePath(relativeFilePath);

            var content = File.ReadAllText(fullPath);

            return content;
        }

        public Task<string> SaveFileToDownloadsFolder(byte[] bytes, String fileName)
        {
            // TODO_RAK: todavia no implementado en iOs

            return new Task<String>(delegate ()
            {
                return String.Empty;
            });
        }
    }
}
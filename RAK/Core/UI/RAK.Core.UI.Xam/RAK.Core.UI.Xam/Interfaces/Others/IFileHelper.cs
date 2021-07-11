using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam
{
    public interface IFileHelper
    {
        /// <summary>
        /// Obtiene el full path de un archivo perteneciente a la carpeta del usuario
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetLocalFilePath(string filename);

        /// <summary>
        /// Obtiene el full path de un archivo (recurso, archivo html, etc) perteneciente al proyecto
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        string GetProjectFilePath(string relativeFilePath);

        /// <summary>
        /// Obtiene el texto de un archivo de recursos
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        string ReadTextAssetFileContent(string relativeFilePath);

        /// <summary>
        /// Obtiene el path de la carpeta downloads
        /// </summary>
        /// <returns></returns>
        Task<string> SaveFileToDownloadsFolder(byte[] bytes, String fileName);
    }
}

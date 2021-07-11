using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RAK.Core.UI.Xam.Helpers
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Indica si la excepcion es HttpStatusCode.Unauthorized
        /// </summary>
        public static bool isUnauthorized(Exception ex)
        {
            bool result = false;

            WebException WebEx = ex as WebException;
            if (WebEx != null && WebEx.Status == WebExceptionStatus.ProtocolError)
            {
                var response = WebEx.Response as HttpWebResponse;

                // -- Cacheo de 'Unauthorized'
                if (response != null && response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    result = true;
                }
            }

            return result;
        }

    }
}

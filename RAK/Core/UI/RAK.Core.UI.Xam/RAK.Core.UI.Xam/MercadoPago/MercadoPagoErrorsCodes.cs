using System;

namespace RAK.Core.UI.Xam.MercadoPago
{

    /// <summary>
    /// Codigos de error MP
    /// </summary>
    public static class MercadoPagoErrorsCodes
    {
        public const string NO_CARDNUMBER_CREATE_TOKEN_ERROR = "205";
        public const string NO_CARDEXPIRATIONMONTH_CREATE_TOKEN_ERROR = "208";
        public const string NO_CARDEXPIRATIONYEAR_CREATE_TOKEN_ERROR = "209";
        public const string NO_DOCTYPE_CREATE_TOKEN_ERROR = "212";
        public const string NO_DOCNUMBER_CREATE_TOKEN_ERROR = "213";
        public const string DOCNUMBER_EMPTY_CREATE_TOKEN_ERROR = "214";
        public const string NO_CARDISSUER_CREATE_TOKEN_ERROR = "220";
        public const string NO_CARDHOLDER_CREATE_TOKEN_ERROR = "221";
        public const string NO_SECURITYCODE_CREATE_TOKEN_ERROR = "224";
        public const string INVALID_CARDNUMBER_CREATE_TOKEN_ERROR = "E301";
        public const string INVALID_SECURITYCODE_CREATE_TOKEN_ERROR = "E302";
        public const string INVALID_CARDHOLDER_CREATE_TOKEN_ERROR = "316";
        public const string INVALID_DOCTYPE_CREATE_TOKEN_ERROR = "322";
        public const string INVALID_SUBDOCTYPE_CREATE_TOKEN_ERROR = "323";
        public const string INVALID_DOCNUMBER_CREATE_TOKEN_ERROR = "324";
        public const string INVALID_CARDEXPIRATIONMONTH_CREATE_TOKEN_ERROR = "325";
        public const string INVALID_CARDEXPIRATIONYEAR_CREATE_TOKEN_ERROR = "326";
        public const string GENERAL_ERROR = "400";

        /// <summary>
        /// Obtiene mensajes genericos segun el Codigo
        /// </summary>
        public static string GetMessageByCode(String Code)
        {
            string msg = string.Empty; 
            switch (Code)
            {
                case NO_CARDNUMBER_CREATE_TOKEN_ERROR:
                    msg = "Ingresa el número de tu tarjeta.";
                    break;
                case NO_CARDEXPIRATIONMONTH_CREATE_TOKEN_ERROR:
                    msg = "Elige un mes.";
                    break;
                case NO_CARDEXPIRATIONYEAR_CREATE_TOKEN_ERROR:
                    msg = "Elige un año.";
                    break;
                case NO_DOCTYPE_CREATE_TOKEN_ERROR:
                case NO_DOCNUMBER_CREATE_TOKEN_ERROR:
                case DOCNUMBER_EMPTY_CREATE_TOKEN_ERROR:
                    msg = "Ingresa tu documento.";
                    break;
                case NO_CARDISSUER_CREATE_TOKEN_ERROR:
                    msg = "Ingresa tu banco emisor.";
                    break;
                case NO_CARDHOLDER_CREATE_TOKEN_ERROR:
                    msg = "Ingresa el nombre y apellido.";
                    break;
                case NO_SECURITYCODE_CREATE_TOKEN_ERROR:
                    msg = "Ingresa el código de seguridad.";
                    break;
                case INVALID_CARDNUMBER_CREATE_TOKEN_ERROR:
                    msg = "Hay algo mal en ese número. Vuelve a ingresarlo.";
                    break;
                case INVALID_SECURITYCODE_CREATE_TOKEN_ERROR:
                    msg = "Revisa el código de seguridad.";
                    break;
                case INVALID_CARDHOLDER_CREATE_TOKEN_ERROR:
                    msg = "Ingresa un nombre válido.";
                    break;
                case INVALID_DOCTYPE_CREATE_TOKEN_ERROR:
                case INVALID_SUBDOCTYPE_CREATE_TOKEN_ERROR:
                case INVALID_DOCNUMBER_CREATE_TOKEN_ERROR:
                    msg = "Revisa tu documento.";
                    break;
                case INVALID_CARDEXPIRATIONMONTH_CREATE_TOKEN_ERROR:
                case INVALID_CARDEXPIRATIONYEAR_CREATE_TOKEN_ERROR:
                    msg = "Revisa la fecha.";
                    break;

                default:
                    msg = "Revisa los datos.";
                    break;
            }

            return msg;
        }

    }

}

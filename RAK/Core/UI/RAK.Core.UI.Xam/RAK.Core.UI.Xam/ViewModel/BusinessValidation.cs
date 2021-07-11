using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAK.Core.UI.Xam.ViewModel
{
    public class BusinessValidation
    {
        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public BusinessValidation()
        {
            // -- Por defecto error.
            this.Style = ValidationStyle.Error;
            this.Guid = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mensaje">mensaje</param>
        public BusinessValidation(string mensaje)
        {
            // -- Por defecto error.
            this.Style = ValidationStyle.Error;
            this.Guid = System.Guid.NewGuid().ToString();
            this.Message = mensaje;
        }

        #endregion

        #region Properties

        public string Guid { get; private set; }

        /// <summary>
        /// Código de error
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Estilo de la Alerta
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Indica si se puede cerrar
        /// </summary>
        public bool Dismissable { get; set; }

        #endregion
    }

    /// <summary>
    /// Tipos de estilo para las alertas
    /// </summary>
    public static class ValidationStyle
    {
        public const string Success = "success";
        public const string Information = "info";
        public const string Warning = "warning";
        public const string Error = "error";
    }

    public class BusinessValidationCollection : List<BusinessValidation>
    {
        /// <summary>
        /// Obtiene si hay errores
        /// </summary>
        public Boolean HasErrors
        {
            get
            {
                return (this.Count(v => v.Style == ValidationStyle.Error) > 0);
            }
        }

        /// <summary>
        /// Obtiene si hay alertas
        /// </summary>
        public Boolean HasWarnings
        {
            get
            {
                return (this.Count(v => v.Style == ValidationStyle.Error) > 0);
            }
        }


        /// <summary>
        /// Agrega un mensaje
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="style"></param>
        public void AddValidation(string message, int code = default(int), string resourceKey = null, string style = ValidationStyle.Error)
        {
            if (String.IsNullOrEmpty(message) && !String.IsNullOrEmpty(resourceKey))
            {
                message = this.GetMessageFromResources(resourceKey);
            }

            var validation = new BusinessValidation()
            {
                Message = message,
                ErrorCode = code,
                Style = style
            };

            this.Add(validation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationList"></param>
        public void AddRange(BusinessValidationCollection validationList)
        {
            this.AddRange(validationList.ToList());
        }

        /// <summary>
        /// Obtiene un mensaje en base a un Key.
        /// </summary>
        /// <param name="errorCode">Codigo de la validacion</param>
        /// <returns>String con el mensaje</returns>
        private string GetMessageFromResources(string code)
        {
            return "";
            //return Resources.businessMessages.ResourceManager.GetString(code);
        }
    }
}

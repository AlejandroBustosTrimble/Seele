using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.ViewModel
{
    public class GenericResponseVM<T>
        where T : RequestViewModelBase
    {
        /// <summary>
        /// Alertas
        /// </summary>
        public BusinessValidationCollection Alerts
        {
            get; set;
        } = new BusinessValidationCollection();

        /// <summary>
        /// Respuesta
        /// </summary>
        public T ResponseVM
        {
            get; set;
        }
    }

    public class GenericListResponseVM<T>
        where T : class
    {
        /// <summary>
        /// Alertas
        /// </summary>
        public BusinessValidationCollection Alerts
        {
            get; set;
        } = new BusinessValidationCollection();

        /// <summary>
        /// Respuesta
        /// </summary>
        public List<T> ResponseVM
        {
            get; set;
        }
    }

}

using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Model
{

    /// <summary>
    /// Interface para marcar requests 
    /// </summary>
    public interface IRequestXam
    {

    }

    /// <summary>
    /// Interface para marcar Response 
    /// </summary>
    public interface IResponseXam
    {

    }

    /// <summary>
    /// Paquete Xam de Rta
    /// </summary>
    public class ResponseXamPackageList<VMRes> : IResponseXamPackageList<VMRes>
    where VMRes : IResponseXam
    {
        public List<BusinessValidation> Alerts { get; set; }
        public List<VMRes> ResponseVM { get; set; }
        public bool Unauthorized { get; set; }
        public bool HasInternet { get; set; } = true;
    }

    /// <summary>
    /// Paquete Xam de Rta
    /// </summary>
    public class ResponseXamPackage<VMRes> : IResponseXamPackage<VMRes>
    where VMRes : IResponseXam
    {
        public List<BusinessValidation> Alerts { get; set; }
        public VMRes ResponseVM { get; set; }
        public bool Unauthorized { get; set; }
        public bool HasInternet { get; set; } = true;
    }

    /// <summary>
    /// Paquete contenedor de la respuesta
    /// </summary>
    public interface IResponseXamPackage<VMRes> : IResponseXamPackageCommon
    where VMRes : IResponseXam
    {
        List<BusinessValidation> Alerts { get; set; }
        VMRes ResponseVM { get; set; }
    }

    /// <summary>
    /// Paquete contenedor de la respuesta
    /// </summary>
    public interface IResponseXamPackageList<VMRes> : IResponseXamPackageCommon
    where VMRes : IResponseXam
    {
        List<BusinessValidation> Alerts { get; set; }
        List<VMRes> ResponseVM { get; set; }
    }

    public interface IResponseXamPackageCommon
    {
        bool Unauthorized { get; set; }
        bool HasInternet { get; set; }
    }


}

using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using System.Web.Http;

namespace RAK.Core.Api.WebApi
{
    /// <summary>
    /// Api de de Entidades de SOLO Consulta
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    public abstract class ConsultWebApiControllerBase<VM, LIVM, LCVM, A> : WebApiControllerBase<A>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where LCVM : class, IListedCriteriaModel

        where A : IConsultAdapter<VM, LIVM, LCVM>
    {
        #region Methods

        #region Public

        [HttpPost]
        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<VM> GetById(IdentityModel modelReq)
        {
            var responseModel = this.ExecuteAction<IdentityModel, VM>(req =>
            {
                var resultData = this.Adapter.GetById(req);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        [HttpPost]
        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<ListModel<LIVM>> GetListed(LCVM modelReq)
        {
            var responseModel = this.ExecuteAction<LCVM, ListModel<LIVM>>(req =>
            {
                var resultData = this.Adapter.GetListed(req);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        [HttpPost]
        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<ListModel<VM>> GetAll(GetListModel modelReq)
        {
            var responseModel = this.ExecuteAction<GetListModel, ListModel<VM>>(req =>
            {
                var resultData = this.Adapter.GetAll(req);

                return (ListModel<VM>)resultData;
            }, modelReq);

            return responseModel;
        }

        #endregion

        #endregion
    }
}

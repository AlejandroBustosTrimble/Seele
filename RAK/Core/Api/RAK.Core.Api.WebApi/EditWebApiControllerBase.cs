using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using System.Web.Http;

namespace RAK.Core.Api.WebApi
{
    /// <summary>
    /// Api de Entidades Editable
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="A"></typeparam>
    public abstract class EditWebApiControllerBase<VM, LIVM, LCVM, A> : ConsultWebApiControllerBase<VM, LIVM, LCVM, A>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where LCVM : class, IListedCriteriaModel

        where A : IEditAdapter<VM, LIVM, LCVM>
    {
        #region Methods

        #region Public

        [HttpPost]
        /// <summary>
        /// Inserta una entidad
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<ResultModel> Add(VM modelReq)
        {
            var responseModel = this.ExecuteAction<VM, ResultModel>(req =>
            {
                var resultData = this.Adapter.Add(req);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        [HttpPost]
        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<ResultModel> Update(VM modelReq)
        {
            var responseModel = this.ExecuteAction<VM, ResultModel>(req =>
            {
                var resultData = this.Adapter.Update(req);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        [HttpPost]
        /// <summary>
        /// Borra una entidad
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<ResultModel> Delete(IdentityModel modelReq)
        {
            var responseModel = this.ExecuteAction<IdentityModel, ResultModel>(req =>
            {
                var resultData = this.Adapter.Delete(modelReq);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        #endregion

        #endregion
    }
}

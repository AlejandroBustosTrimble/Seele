using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using System.Web.Http;

namespace RAK.Core.Api.WebApi
{
    /// <summary>
    /// Api de Entidades de SOLO Consulta con vista basica
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="BIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="BCVM"></typeparam>
    /// <typeparam name="A"></typeparam>
    public abstract class FullConsultWebApiControllerBase<VM, LIVM, BIVM, LCVM, BCVM, A> : ConsultWebApiControllerBase<VM, LIVM, LCVM, A>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where BIVM : class, IBasicModel
        where LCVM : class, IListedCriteriaModel
        where BCVM : class, IBasicCriteriaModel
        where A : IFullConsultAdapter<VM, LIVM, BIVM, LCVM, BCVM>
    {
        #region Methods

        #region Public

        [HttpPost]
        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="modelReq"></param>
        /// <returns></returns>
        public IApiResponseModel<IListModel<BIVM>> GetBasic(BCVM modelReq)
        {
            var responseModel = this.ExecuteAction<BCVM, IListModel<BIVM>>(req =>
            {
                var resultData = this.Adapter.GetBasic(req);

                return resultData;
            }, modelReq);

            return responseModel;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Api de Entidades de SOLO Consulta con vista basica
    /// </summary>
    /// <remarks>Ya tiene por defecto las entidades para GetBasic</remarks>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="BIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="BCVM"></typeparam>
    /// <typeparam name="A"></typeparam>
    public abstract class FullConsultWebApiControllerBase<VM, LIVM, LCVM, A> : ConsultWebApiControllerBase<VM, LIVM, LCVM, A>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where LCVM : class, IListedCriteriaModel
        where A : IFullConsultAdapter<VM, LIVM, BasicModel, LCVM, BasicCriteriaModel>
    {
    }
}

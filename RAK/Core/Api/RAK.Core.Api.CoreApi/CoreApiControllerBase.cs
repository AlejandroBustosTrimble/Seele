using Microsoft.AspNetCore.Mvc;
using RAK.Core.Api.Model.Containers;
using RAK.Core.Api.Model.Interfaces;
using RAK.Fwk.Api.Adapter;
using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Common.Log;
using System;

namespace RAK.Core.Api.CoreApi
{
    /// <summary>
    /// Controller Api Web Core Base
    /// </summary>
    /// <typeparam name="A"></typeparam>
    public abstract class CoreApiControllerBase<TAdapter> : ControllerBase
        where TAdapter : IAdapterGeneric
    {
        #region Members

        private TAdapter adapter;

        #endregion

        #region Properties

        /// <summary>
        /// Logica
        /// </summary>
        protected TAdapter Adapter
        {
            get
            {
                if (this.adapter == null)
                {
                    this.adapter = DIEngineContainer.Instance.Resolve<TAdapter>();
                }

                return this.adapter;
            }
        }

        #endregion

        #region Methods

        #region Protected

        /// <summary>
        /// Ejecuta una accion
        /// </summary>
        /// <typeparam name="REQ"></typeparam>
        /// <typeparam name="RES"></typeparam>
        /// <param name="funcToRun"></param>
        /// <param name="req"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IApiResponseModel<TDataResponseVM> ExecuteAction<REQVM, TDataResponseVM>(Func<REQVM, TDataResponseVM> funcToRun, REQVM reqVM)
            where REQVM : IModelBase
            where TDataResponseVM : IResponseModel
        {
            var response = new ApiResponseModel<TDataResponseVM>();

            try
            {
                var resModel = funcToRun(reqVM);
                response.Data = resModel;
            }
            catch (Exception ex)
            {
                LoggerContainer.Instance.AddErrorLog(ex);

                var alert = new ApiAlertModel();
                //var alert = DIEngineContainer.Instance.Resolve<IApiAlertModel>();
                // TODO_RAK: Hacer tipos de error, para que en este caso pueda sacar el codigo de error por ej
                // Tambien para setear el Unique identifier que lo voy a mostrar por pantalla para que el usuario cuando tenga un e
                // Error no controlado pueda buscar el error directamente

                alert.Message = ex.Message;

                response.AlertList.Add(alert);
            }

            return response;
        }

        #endregion

        #endregion
    }
}

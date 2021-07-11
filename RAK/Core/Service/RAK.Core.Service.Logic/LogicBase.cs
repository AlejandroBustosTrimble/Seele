using System;
using RAK.Core.Service.Logic.UnitOfWorks;
using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Service.Logic;
using RAK.Fwk.Service.Repository.Abstraction;

namespace RAK.Core.Service.Logic
{
    /// <summary>
    /// Logica base
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public abstract class LogicBase<R> : LogicGeneric<R>
        where R : IRepositoryGeneric
    {
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
        protected RES ExecuteAction<REQ, RES>(Func<REQ, RES> funcToRun, REQ req, UnitOfWorkParam param = null)
            where RES: new()
        {
            RES response = new RES();

            try
            {
                var tempParam = param;
                if (tempParam == null)
                {
                    // TODO_RAK: el timeout por defecto deberia estar en una propiedad abstract, etc
                    tempParam = new UnitOfWorkParam() { Timeout = 10000, UseTransaction = true };
                }

                using (var uow = UnitOfWorks.UnitOfWorkContainer.GetInstance(this.Repository, tempParam))
                {
                    response = funcToRun(req);

                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                // No logueo aca porque lo hago en el adapter que es el punto de entrada para las acciones, cosa de que log se
                // realice una sola vez por error, porque si una logic llama a otra y da un error la logic interna
                // ese error se loguearia 2 veces
                // Dejo el catch con fines de Debugueo nada mas
                throw;
            }

            return response;
        }

        #endregion

        #endregion
    }
}

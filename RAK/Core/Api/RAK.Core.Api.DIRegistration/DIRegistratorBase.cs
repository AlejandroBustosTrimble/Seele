using RAK.Fwk.Api.Adapter;
using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Common.Log;
using RAK.Fwk.Service.Logic.Abstraction;
using RAK.Fwk.Service.Repository.Abstraction;
using System;
using System.Reflection;

namespace RAK.Core.Api.DIRegistration
{
    /// <summary>
    /// Registrador de dependencias
    /// </summary>
    public abstract class DIRegistratorBase
    {
        #region Methods

        #region Public

        /// <summary>
        /// Registra dependencias
        /// </summary>
        public virtual void Register()
        {
            #region Registro los componentes de las capas

            // -- Registro los Adapters de la aplicacion en si
            DIEngineContainer.Instance.RegisterAssembly<IAdapterGeneric>(this.GetAdapterAssembly());

            // -- Registro las Logicas de la aplicacion en si
            DIEngineContainer.Instance.RegisterAssembly<ILogicGeneric>(this.GetLogicAssembly());

            // -- Registro los Repositorios de la aplicacion en si
            DIEngineContainer.Instance.RegisterAssembly<IRepositoryGeneric>(this.GetRepositoryAssembly());

            #endregion

            #region Registro tipos adicionales

            // TODO_RAK: sacar if
            var loggerType = this.GetLoggerType();
            if (loggerType != null)
            {
                DIEngineContainer.Instance.Register<ILogger>(this.GetLoggerType());
            }

            DIEngineContainer.Instance.Register<IRepositoryContext>(this.GetRepositoryContextType());

            this.CustomRegister();

            #endregion
        }

        #endregion

        #region Protected

        /// <summary>
        /// Registraciones Customs adicionales
        /// </summary>
        protected virtual void CustomRegister()
        {

        }

        /// <summary>
        /// Assembly de Models de la aplicacion
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetModelAssembly();

        /// <summary>
        /// Assembly de Entities de la aplicacion
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetEntityAssembly();

        /// <summary>
        /// Assembly de Adapters de la aplicacion
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetAdapterAssembly();

        /// <summary>
        /// Assembly de Logics de la aplicacion
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetLogicAssembly();

        /// <summary>
        /// Assembly de Repositories de la aplicacion
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetRepositoryAssembly();

        /// <summary>
        /// Tipo de Logger
        /// </summary>
        /// <returns></returns>
        protected abstract Type GetLoggerType();

        /// <summary>
        /// Tipo de Contexto de Repository
        /// </summary>
        /// <returns></returns>
        protected abstract Type GetRepositoryContextType();

        #endregion

        #endregion
    }

    public enum DIRegistrationSectionEnum
    {
        Models,
        Entities
    }
}

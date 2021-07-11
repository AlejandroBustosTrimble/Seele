using OFIUM.App.Api.Adapter;
using OFIUM.App.Api.Model;
using OFIUM.App.Service.Entity;
using OFIUM.App.Service.Logic;
using OFIUM.App.Service.Repository;
using OFIUM.App.Service.Repository.Database;
using RAK.Core.Api.DIRegistration;
using System;
using System.Reflection;

namespace OFIUM.App.Api.DIRegistration
{
    /// <summary>
    /// Registrador de dependencias de OFIUM
    /// </summary>
    public class DIRegistrator : DIRegistratorBase
    {
        /// <summary>
        /// Assembly de los modelos
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetModelAssembly()
        {
            var assembly = typeof(ReceiptTypeM).Assembly;

            return assembly;
        }

        /// <summary>
        /// Assembly de las entidades
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetEntityAssembly()
        {
            var assembly = typeof(ReceiptType).Assembly;

            return assembly;
        }

        /// <summary>
        /// Assembly de los adapters
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetAdapterAssembly()
        {
            var assembly = typeof(ReceiptTypeAdapter).Assembly;

            return assembly;
        }

        /// <summary>
        /// Tipo del Logger
        /// </summary>
        /// <returns></returns>
        protected override Type GetLoggerType()
        {
            return null;
        }

        /// <summary>
        /// Assembly de Logics
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetLogicAssembly()
        {
            var assembly = typeof(ReceiptTypeLogic).Assembly;

            return assembly;
        }

        /// <summary>
        /// Assembly de Repository
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetRepositoryAssembly()
        {
            var assembly = typeof(ReceiptTypeRepository).Assembly;

            return assembly;
        }

        /// <summary>
        /// Tipo de contexto de repository
        /// </summary>
        /// <returns></returns>
        protected override Type GetRepositoryContextType()
        {
            return typeof(RepositoryContext);
        }
    }
}

using ExamInt.App.Api.Adapter;
using ExamInt.App.Api.Model;
using ExamInt.App.Service.Entity;
using ExamInt.App.Service.Logic;
using ExamInt.App.Service.Repository;
using ExamInt.App.Service.Repository.Database;
using RAK.Core.Api.DIRegistration;
using System;
using System.Reflection;

namespace ExamInt.App.Api.DIRegistration
{
    /// <summary>
    /// ExamInt DIRegistrator
    /// </summary>
    public class DIRegistrator : DIRegistratorBase
    {
        /// <summary>
        /// Models Assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetModelAssembly()
        {
            var assembly = typeof(PermissionM).Assembly;

            return assembly;
        }

        /// <summary>
        /// Entities Assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetEntityAssembly()
        {
            var assembly = typeof(Permission).Assembly;

            return assembly;
        }

        /// <summary>
        /// Adapters Assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetAdapterAssembly()
        {
            var assembly = typeof(PermissionAdapter).Assembly;

            return assembly;
        }

        /// <summary>
        /// Logger type
        /// </summary>
        /// <returns></returns>
        protected override Type GetLoggerType()
        {
            return null;
        }

        /// <summary>
        /// Logics Assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetLogicAssembly()
        {
            var assembly = typeof(PermissionLogic).Assembly;

            return assembly;
        }

        /// <summary>
        /// Repositories Assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetRepositoryAssembly()
        {
            var assembly = typeof(PermissionRepository).Assembly;

            return assembly;
        }

        /// <summary>
        /// RepositoryContext Type
        /// </summary>
        /// <returns></returns>
        protected override Type GetRepositoryContextType()
        {
            return typeof(RepositoryContext);
        }
    }
}

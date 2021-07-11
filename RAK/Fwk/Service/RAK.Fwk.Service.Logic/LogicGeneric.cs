using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Service.Logic.Abstraction;
using RAK.Fwk.Service.Repository.Abstraction;

namespace RAK.Fwk.Service.Logic
{
    /// <summary>
    /// Logica generica
    /// </summary>
    public abstract class LogicGeneric<R> : ILogicGeneric
        where R : IRepositoryGeneric
    {
        #region Members

        private R repo;

        #endregion

        #region Properties

        /// <summary>
        /// Repositorio de datos
        /// </summary>
        protected virtual R Repository
        {
            get
            {
                if(this.repo == null)
                {
                    this.repo = DIEngineContainer.Instance.Resolve<R>();
                }

                return this.repo;
            }
        }

        #endregion
    }
}

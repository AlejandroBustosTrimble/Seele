using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Service.Repository.Abstraction;

namespace RAK.Fwk.Service.Repository
{
    /// <summary>
    /// Container de RepositoryContext
    /// </summary>
    public static class RepositoryContextContainer
    {
        private static IRepositoryContext instance;

        /// <summary>
        /// Singleton del Contexto de Repositorios
        /// </summary>
        public static IRepositoryContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = DIEngineContainer.Instance.Resolve<IRepositoryContext>();
                }

                return instance;
            }
        }
    }
}

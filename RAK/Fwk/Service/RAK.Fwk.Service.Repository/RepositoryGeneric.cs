using RAK.Fwk.Service.Repository.Abstraction;
using System.Transactions;

namespace RAK.Fwk.Service.Repository
{
    /// <summary>
    /// Repositorio generico
    /// </summary>
    public abstract class RepositoryGeneric : IRepositoryGeneric
    {
        #region Properties

        /// <summary>
        /// Contexto
        /// </summary>
        protected IRepositoryContext Context
        {
            get
            {
                return RepositoryContextContainer.Instance;
            }
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Cierra la conexion
        /// </summary>
        public virtual void CloseConnection()
        {
            this.Context.CloseConnection();
        }

        /// <summary>
        /// Inicia una conexion
        /// </summary>
        /// <param name="transactionScope"></param>
        public void BeginConnection(TransactionScope transactionScope)
        {
            this.Context.BeginConnection(transactionScope);
        }

        #endregion

        #endregion
    }
}

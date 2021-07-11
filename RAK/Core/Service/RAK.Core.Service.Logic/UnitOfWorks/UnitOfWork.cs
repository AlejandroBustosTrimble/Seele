using RAK.Fwk.Service.Repository.Abstraction;
using System;
using System.Transactions;

namespace RAK.Core.Service.Logic.UnitOfWorks
{
    /// <summary>
    /// Unit Of Work
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Properties
        
        private UnitOfWorkParam Param { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IRepositoryGeneric Repository { get; set; }

        /// <summary>
        /// TransactionScope
        /// </summary>
        private TransactionScope TransactionScope { get; set; }

        /// <summary>
        /// Indice (0 es el primero)
        /// </summary>
        private int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool ShouldExecute
        {
            get
            {
                // Al ejecutar un metodo de una logica, se crea un UnitOfWork que da un contexto transaccional
                // Pero como el ORM que usamos no soporta esto, agregue la posibilidad de tener anidados UnitOfWorks
                // desde el lado de la logica, pero el que realiza verdaderamente el commit es el primero
                // Si se cambia el ORM a uno que soporte anidamiento, se saca esto
                return this.Index == 0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pRepository"></param>
        /// <param name="pParam"></param>
        internal UnitOfWork(IRepositoryGeneric pRepository, UnitOfWorkParam pParam, int index)
        {
            this.Repository = pRepository;
            this.Param = pParam;
            this.Index = index;

            if(this.ShouldExecute)
            {
                if (this.Param.UseTransaction)
                {
                    this.BeginTransactionContext();
                }

                this.BeginConnection(); // TODO_RAK: arreglar el tema de que encuentra NHibernate
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            if (this.ShouldExecute)
            {
                // -- Solamente hacemos el Complete en el caso de que el contexto UOW sea con transaccion. 
                if (this.TransactionScope != null)
                {
                    this.TransactionScope.Complete();
                }
            } // TODO_RAK: descomentar
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            //TODO_RAK: descomentar
            if (this.ShouldExecute)
            {
                if (this.TransactionScope != null)
                {
                    this.TransactionScope.Dispose();
                }

                this.Repository.CloseConnection();
            }

            UnitOfWorkContainer.RemoveInstance(this);
        }

        #region Protected

        /// <summary>
        /// Inicia un Contexto de Transaccion para la Unidad de Trabajo.
        /// </summary>
        private void BeginTransactionContext()
        {
            var txOptions = new TransactionOptions();

            txOptions.IsolationLevel = this.Param.IsolationLevel;

            txOptions.Timeout = new TimeSpan(0, 0, this.Param.Timeout);

            this.TransactionScope = new TransactionScope(TransactionScopeOption.Required, txOptions);
        }

        /// <summary>
        /// Inicia la conexion.
        /// </summary>
        private void BeginConnection()
        {
            this.Repository.BeginConnection(this.TransactionScope);
        }

        #endregion

        #endregion
    }
}

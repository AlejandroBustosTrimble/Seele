using System.Transactions;

namespace RAK.Fwk.Service.Repository.Abstraction
{
    /// <summary>
    /// Interfaz de repositorio
    /// </summary>
    public interface IRepositoryGeneric
    {
        /// <summary>
        /// Inicia la conexion
        /// </summary>
        /// <param name="transactionScope">Transaccion, si es null entonces no esta bajo transaccion</param>
        void BeginConnection(TransactionScope transactionScope);

        /// <summary>
        /// Cierra la conexion
        /// </summary>
        void CloseConnection();
    }
}

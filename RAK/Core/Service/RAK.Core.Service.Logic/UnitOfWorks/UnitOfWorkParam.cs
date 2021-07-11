using System.Transactions;

namespace RAK.Core.Service.Logic.UnitOfWorks
{
    public class UnitOfWorkParam
    {
        /// <summary>
        /// Timeout en milisegundos
        /// </summary>
        public int Timeout { get; set; }

        public bool UseTransaction { get; set; }

        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadUncommitted;
    }
}

using RAK.Fwk.Service.Repository.Abstraction;
using System.Collections.Generic;

namespace RAK.Core.Service.Logic.UnitOfWorks
{
    /// <summary>
    /// Container de UnitOfWork
    /// </summary>
    public static class UnitOfWorkContainer
    {
        /// <summary>
        /// Lista de UnitOfWork que estan corriendo
        /// </summary>
        private static List<UnitOfWork> UnitOfWorkList { get; } = new List<UnitOfWork>();

        /// <summary>
        /// Obtiene una instancia
        /// </summary>
        /// <param name="pRepository"></param>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public static UnitOfWork GetInstance(IRepositoryGeneric pRepository, UnitOfWorkParam pParam)
        {
            var uow = new UnitOfWork(pRepository, pParam, UnitOfWorkList.Count);

            UnitOfWorkList.Add(uow);

            return uow;
        }

        /// <summary>
        /// Borra una instancia
        /// </summary>
        /// <param name="uow"></param>
        internal static void RemoveInstance(UnitOfWork uow)
        {
            UnitOfWorkList.Remove(uow);
        }
    }
}

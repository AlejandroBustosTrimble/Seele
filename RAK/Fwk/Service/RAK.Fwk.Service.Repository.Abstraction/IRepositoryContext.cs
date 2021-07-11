using RAK.Fwk.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;

namespace RAK.Fwk.Service.Repository.Abstraction
{
    /// <summary>
    /// Contexto a nivel Repository
    /// (Acceso a los datos por BD (Apertura o Cierre de conexiones, transacciones, etc), etc)
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        T GetById<T>(long ID)
            where T : class, IEntity;

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAll<T>(int? count, int? offset)
            where T : class, IEntity;


        /// <summary>
        /// Obtiene una lista de una entidad en base a un filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria, int? count, int? offset)
            where T : class, IEntity;


        /// <summary>
        /// Inserta / Actualiza la entidad
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="entity">Entidad</param>
        Int64 Add<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Para agregar masivamente una lista de entidades, retorna los ids insertados en base de datos
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entites">Lista de Entidades</param>
        /// <returns>List<NuevosID></returns>
        List<long> AddOrUpdateBatch<T>(List<T> entites) where T : class, IEntity;

        /// <summary>
        /// Elimina entidades masivamente
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="entites">Lista de Entidades</param>
        Boolean DeleteBatch<T>(List<T> entites) where T : class, IEntity;

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="entity">Instancia</param>
        Boolean Update<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Boolean Delete<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Delete en SQL (NO es un delete logico, borra el registro)
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="ID">ID de la Entidad a eliminar</param>
        Boolean DeleteByID<T>(long ID) where T : class, IEntity;

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

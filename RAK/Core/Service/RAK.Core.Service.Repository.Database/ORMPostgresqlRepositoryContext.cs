using FluentNHibernate.Cfg.Db;
using NHibernate;
using Npgsql;
using System.Transactions;

namespace RAK.Core.Service.Repository.Database
{
    /// <summary>
    /// Contexto de ORM Postgresql
    /// </summary>
    public abstract class ORMPostgresqlRepositoryContext : ORMRepositoryContext
    {
        /// <summary>
        /// Obtiene el configurador
        /// </summary>
        /// <param name="cnnStr"></param>
        /// <returns></returns>
        protected override IPersistenceConfigurer GetConfigurer(string cnnStr)
        {
            return PostgreSQLConfiguration.Standard.ConnectionString(cnnStr);
        }

        /// <summary>
        /// Vincula una transaccion en la session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="transaction"></param>
        protected override void EnlistTransaction(ISession session, Transaction transaction)
        {
            ((NpgsqlConnection)session.Connection).EnlistTransaction(transaction);
        }
    }
}

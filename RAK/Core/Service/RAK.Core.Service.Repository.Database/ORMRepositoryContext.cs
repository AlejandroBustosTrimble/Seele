using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using RAK.Core.Service.Repository.Database.Conventions;
using RAK.Fwk.Service.Entity;
using RAK.Fwk.Service.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;

namespace RAK.Core.Service.Repository.Database
{
    /// <summary>
    /// Contexto ORM a bd
    /// </summary>
    public abstract class ORMRepositoryContext : IRepositoryContext
    {
        #region Members

        /// <summary>
        /// Session Factory
        /// </summary>
        private ISessionFactory sessionFactory;

        private ISession session;

        #endregion

        #region Properties

        /// <summary>
        /// Key de la cadena de Conexion
        /// </summary>
        protected abstract String ConnectionStringKey
        {
            get;
        }

        /// <summary>
        /// Nombre del assembly que contiene los mapeos
        /// </summary>
        protected abstract Assembly MappingAssembly { get; }

        private ISessionFactory SessionFactory
        {
            get
            {
                if (this.sessionFactory == null)
                {
                    // TODO_RAK
                    var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                    IPersistenceConfigurer config = this.GetConfigurer(connStr);

                    sessionFactory = IsWebContext ?

                         Fluently.Configure()
                            .CurrentSessionContext<WebSessionContext>()
                            .Database(config)
                            .Mappings(m =>
                            {
                                m.FluentMappings
                                    .AddFromAssembly(this.MappingAssembly)
                                    //.Conventions.Add<LowercaseTableNameConvention>()
                                    ;
                            })
                            .ExposeConfiguration(this.BuildSchema)
                        //.BuildConfiguration()
                        .BuildSessionFactory() :


                        Fluently.Configure()
                            .CurrentSessionContext<ThreadStaticSessionContext>()
                            .Database(config)
                            .Mappings(m =>
                            {
                                m.FluentMappings
                                    .AddFromAssembly(this.MappingAssembly)
                                    //.Conventions.Add<LowercaseTableNameConvention>();
                                    ;
                            })
                            //.BuildConfiguration()
                        .BuildSessionFactory();
                }

                return this.sessionFactory;
            }
        }

        /// <summary>
        /// ISession -> Retorna siempre una sesión activa y abierta
        /// </summary>
        private ISession Session
        {
            get
            {
                if (this.session == null)
                {
                    if (!CurrentSessionContext.HasBind(this.SessionFactory))
                    {
                        this.session = this.SessionFactory.OpenSession();
                        CurrentSessionContext.Bind(this.session);
                        this.session.FlushMode = FlushMode.Commit;

                        if (System.Transactions.Transaction.Current != null)
                        {
                            this.EnlistTransaction(session, System.Transactions.Transaction.Current);
                        }
                    }
                }
                return this.session;
            }
        }

        /// <summary>
        /// Indica si se esta corriendo sobre un contexto web o no.
        /// </summary>
        private bool IsWebContext
        {
            get
            {
                return (ReflectiveHttpContext.HttpContextCurrentGetter() != null);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        public ORMRepositoryContext() : base()
        {

        }

        #endregion

        #region Methods

        #region Protected

        /// <summary>
        /// Obtiene el configurador
        /// </summary>
        /// <param name="cnnStr"></param>
        /// <returns></returns>
        protected abstract IPersistenceConfigurer GetConfigurer(string cnnStr);

        /// <summary>
        /// Vincula una transaccion con la sesion actual
        /// </summary>
        protected abstract void EnlistTransaction(ISession session, System.Transactions.Transaction transaction);

        /// <summary>
        /// Bindea la Session.
        /// </summary>
        protected void BindSession()
        {
            // TODO_RAK: verificar que si al iniciar paso por aca, porque me parece que ya bindie la sesion
            // entonces no tendria sentido esto
            if (!CurrentSessionContext.HasBind(this.SessionFactory))
            {
                this.Session.FlushMode = FlushMode.Manual;
            }
        }

        /// <summary>
        /// Construye la bd
        /// </summary>
        /// <param name="config"></param>
        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            var se = new SchemaUpdate(config);
            //se.SetDelimiter(";");
            //se.SetOutputFile(@"c:\bkp\OFIUM.sql");
            se.Execute(true, true);
        }

        #endregion

        #region Public

        /// <summary>
        /// Retorna la conexion.
        /// </summary>
        /// <typeparam name="T">Tipo de la Conexion</typeparam>
        public DbConnection GetConnection()
        {
            return this.SessionFactory.GetCurrentSession().Connection;
        }

        /// <summary>
        /// Cierra la conexion
        /// </summary>
        public void CloseConnection()
        {
            if (this.session != null)
            {
                this.Session.Close();
                this.Session.Dispose();
                this.session = null;
            }
        }

        /// <summary>
        /// Inicia la conexion
        /// </summary>
        /// <param name="transactionScope"></param>
        public void BeginConnection(TransactionScope transactionScope)
        {
            this.BindSession();

            if (transactionScope != null)
            {
                this.GetConnection().EnlistTransaction(Transaction.Current);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetById<T>(long ID)
            where T : class, IEntity
        {
            try
            {
                return this.Session.Get<T>(ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una lista de todas las instancias una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(int? count, int? offset)
            where T : class, IEntity
        {
            try
            {
                IQueryOver<T> query = this.Session.QueryOver<T>();

                if (count.HasValue)
                {
                    query = query.Take(count.Value);
                }

                if (offset.HasValue)
                {
                    query = query.Skip(offset.Value);
                }

                return query.List<T>().ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una lista de todas las instancias una entidad aplicando un filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria, int? count, int? offset)
            where T : class, IEntity
        {
            try
            {
                IQueryOver<T> query = this.Session.QueryOver<T>().Where(criteria);

                if (count.HasValue)
                {
                    query = query.Take(count.Value);
                }

                if (offset.HasValue)
                {
                    query = query.Skip(offset.Value);
                }

                return query.List().ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public Int64 Add<T>(T entity)
            where T : class, IEntity
        {
            //this.Session.Flush(); // TODO_RAK: hacer que flushee al comitear y que el metodo devuelva el ID
            this.Session.SaveOrUpdate(entity);

            return entity.ID;
        }

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public bool Update<T>(T entity)
            where T : class, IEntity
        {
            this.Session.Merge(entity);

            return true;
        }

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Delete<T>(T entity)
            where T : class, IEntity
        {
            this.Session.Delete(entity);

            return true;
        }

        /// <summary>
        /// Agrega una lista de entidades
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entites"></param>
        /// <returns></returns>
        public List<long> AddOrUpdateBatch<T>(List<T> entites)
            where T : class, IEntity
        {
            // -- Retorno los ID's creados
            var ids = new List<long>();

            foreach (var entity in entites)
            {
                Add(entity);
                ids.Add(entity.ID);
            }

            return ids;
        }

        /// <summary>
        /// Elimina una lista de entidades
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entites"></param>
        public bool DeleteBatch<T>(List<T> entites)
            where T : class, IEntity
        {
            foreach (var entity in entites)
            {
                this.Delete(entity);
            }

            return true;
        }

        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        public Boolean DeleteByID<T>(long ID)
            where T : class, IEntity
        {
            var entity = this.GetById<T>(ID);

            this.Delete(entity);

            return true;
        }

        #endregion

        #endregion
    }
}

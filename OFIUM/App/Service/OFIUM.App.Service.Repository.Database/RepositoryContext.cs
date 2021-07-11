using OFIUM.App.Service.Entity;
using OFIUM.App.Service.Repository.Map;
using RAK.Core.Service.Repository.Database;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OFIUM.App.Service.Repository.Database
{
    /// <summary>
    /// Contexto del repositorio
    /// </summary>
    public class RepositoryContext : ORMPostgresqlRepositoryContext
    {
        protected override string ConnectionStringKey
        {
            get
            {
                return null;
            }
        }

        protected override Assembly MappingAssembly
        {
            get
            {
                return Assembly.GetAssembly(typeof(ReceiptTypeMap));
            }
        }
    }
}

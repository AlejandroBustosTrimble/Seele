using ExamInt.App.Service.Repository.Map;
using RAK.Core.Service.Repository.Database;
using System.Reflection;

namespace ExamInt.App.Service.Repository.Database
{
    /// <summary>
    /// Repository context
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
                return Assembly.GetAssembly(typeof(PermissionMap));
            }
        }
    }
}

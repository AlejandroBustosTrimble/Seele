using ExamInt.App.Service.Entity;
using FluentNHibernate.Mapping;

namespace ExamInt.App.Service.Repository.Map
{
    /// <summary>
    /// Permission map
    /// </summary>
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Table("permissions");
            Id(x => x.ID).Column("id_permission").CustomSqlType("BIGSERIAL");
            Map(x => x.Name).Column("name");
            Map(x => x.Surname).Column("surname");
            Map(x => x.RequestDateTime).Column("request_datetime");

            Map(x => x.CreateDateTime).Column("create_datetime").Default("now()");// TODO_RAK: hacer un ORMBaseClassMap, que incluya CreateDateTime, UpdateDateTime y Active
            Map(x => x.UpdateDateTime).Column("update_datetime");

            Map(x => x.Active).Column("active").Default("true");
        }
    }
}

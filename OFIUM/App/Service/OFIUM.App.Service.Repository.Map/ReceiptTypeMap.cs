using FluentNHibernate.Mapping;
using OFIUM.App.Service.Entity;
using System;

namespace OFIUM.App.Service.Repository.Map
{
    public class ReceiptTypeMap : ClassMap<ReceiptType>
    {
        public ReceiptTypeMap()
        {
            Table("receipt_types");
            Id(x => x.ID).Column("id_receipt_type").CustomSqlType("BIGSERIAL");
            Map(x => x.Name).Column("name");
            Map(x => x.Letter).Column("letter");
            Map(x => x.Sign).Column("sign");

            Map(x => x.CreateDateTime).Column("create_datetime").Default("now()");// TODO_RAK: hacer un ORMBaseClassMap
            Map(x => x.UpdateDateTime).Column("update_datetime");

            Map(x => x.Active).Column("active").Default("true");

        }
    }
}

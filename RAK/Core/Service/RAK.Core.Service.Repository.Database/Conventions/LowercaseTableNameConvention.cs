using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.Service.Repository.Database.Conventions
{
    public class LowercaseTableNameConvention : IClassConvention, IClassConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => x.TableName, Is.Not.Set);
        }

        public void Apply(IClassInstance instance)
        {
            instance.Table("tbl_" + instance.EntityType.Name);
        }
    }
}

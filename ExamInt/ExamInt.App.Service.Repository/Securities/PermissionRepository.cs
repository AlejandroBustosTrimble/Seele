using ExamInt.App.Service.Entity;
using ExamInt.App.Service.Repository.Abstraction;
using RAK.Core.Service.Entity;
using RAK.Core.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamInt.App.Service.Repository
{
    /// <summary>
    /// Permission repository
    /// </summary>
    public class PermissionRepository : ConsultRepositoryBase<Permission, PermissionListedItem, PermissionListedCriteria>, IPermissionRepository
    {
        #region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        public PermissionRepository() : base()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene una lista de entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ListEntity<PermissionListedItem> GetListed(PermissionListedCriteria entity)
        {
            //// TODO_RAK:
            var tempList = this.GetAll(new GetListEntity());
            var list = new ListEntity<PermissionListedItem>();

            foreach (var item in tempList.List)
            {
                list.List.Add(new PermissionListedItem()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Surname = item.Surname,
                    RequestDateTime = item.RequestDateTime,
                });
            }

            return list;
        }

        #endregion
    }
}

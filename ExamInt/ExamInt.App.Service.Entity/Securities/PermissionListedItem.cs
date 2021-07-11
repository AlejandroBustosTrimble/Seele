using ExamInt.App.Common.AbstractEntity;
using RAK.Core.Service.Entity;
using System;

namespace ExamInt.App.Service.Entity
{
    /// <summary>
    /// Permission list item
    /// </summary>
    public class PermissionListedItem : ListedItemEntity, IPermissionListedItem
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Permission request datetime
        /// </summary>
        public DateTime? RequestDateTime { get; set; }
    }
}

using ExamInt.App.Common.AbstractEntity;
using RAK.Core.Service.Entity;
using System;

namespace ExamInt.App.Service.Entity
{
    /// <summary>
    /// Permission
    /// </summary>
    public class Permission : EntityBase, IPermission
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Employee Surname
        /// </summary>
        public virtual String Surname { get; set; }

        /// <summary>
        /// Permission request datetime
        /// </summary>
        public virtual DateTime? RequestDateTime { get; set; }
        
    }
}

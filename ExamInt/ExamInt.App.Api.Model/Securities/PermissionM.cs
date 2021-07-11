using ExamInt.App.Common.AbstractEntity;
using RAK.Core.Api.Model;
using System;

namespace ExamInt.App.Api.Model
{
    /// <summary>
    /// Permission
    /// </summary>
    public class PermissionM : SpecificModelBase, IPermission
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Employee Surname
        /// </summary>
        public String Surname { get; set; }

        ///// <summary>
        ///// Employee Surname
        ///// </summary>
        //public String Surname { get; set; }//TODO_RAK

        /// <summary>
        /// Permission request datetime
        /// </summary>
        public DateTime? RequestDateTime { get; set; }
    }
}

using System;

namespace ExamInt.App.Common.AbstractEntity
{
    /// <summary>
    /// Permission list item interface
    /// </summary>
    public interface IPermissionListedItem
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Employee Surname
        /// </summary>
        String Surname { get; set; }

        /// <summary>
        /// Permission request datetime
        /// </summary>
        DateTime? RequestDateTime { get; set; }
    }
}

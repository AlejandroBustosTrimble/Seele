using RAK.Fwk.Common.AbstractEntity;
using System;

namespace ExamInt.App.Common.AbstractEntity
{
    /// <summary>
    /// Permission Entity/Model Interface
    /// </summary>
    public interface IPermission : IEntityGeneric
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Employee Surname
        /// </summary>
        String Surname { get; set; }

        ///// <summary>
        ///// Employee Surname
        ///// </summary>
        //public String Surname { get; set; }//TODO_RAK

        /// <summary>
        /// Permission request datetime
        /// </summary>
        DateTime? RequestDateTime { get; set; }
    }
}

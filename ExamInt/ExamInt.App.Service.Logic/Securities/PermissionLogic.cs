using ExamInt.App.Service.Entity;
using ExamInt.App.Service.Logic.Abstraction;
using ExamInt.App.Service.Repository.Abstraction;
using RAK.Core.Service.Logic;

namespace ExamInt.App.Service.Logic
{
    /// <summary>
    /// Permission Logic
    /// </summary>
    public class PermissionLogic : ConsultLogicBase<Permission, PermissionListedItem, PermissionListedCriteria, IPermissionRepository>, IPermissionLogic
    {
    }
}

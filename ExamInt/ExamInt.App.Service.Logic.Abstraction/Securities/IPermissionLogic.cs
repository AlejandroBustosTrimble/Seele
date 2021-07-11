using ExamInt.App.Service.Entity;
using RAK.Core.Service.Logic.Abstraction;

namespace ExamInt.App.Service.Logic.Abstraction
{
    /// <summary>
    /// Permission logic interface
    /// </summary>
    public interface IPermissionLogic : IConsultLogic<Permission, PermissionListedItem, PermissionListedCriteria>
    {
    }
}

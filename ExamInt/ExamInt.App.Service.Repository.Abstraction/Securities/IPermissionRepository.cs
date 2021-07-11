using ExamInt.App.Service.Entity;
using RAK.Core.Service.Repository.Abstraction;

namespace ExamInt.App.Service.Repository.Abstraction
{
    /// <summary>
    /// Permission repository interface
    /// </summary>
    public interface IPermissionRepository : IConsultRepository<Permission, PermissionListedItem, PermissionListedCriteria>
    {
    }
}

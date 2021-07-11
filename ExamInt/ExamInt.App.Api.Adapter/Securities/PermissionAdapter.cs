using ExamInt.App.Api.Adapter.Abstraction;
using ExamInt.App.Api.Model;
using ExamInt.App.Service.Entity;
using ExamInt.App.Service.Logic.Abstraction;
using RAK.Core.Api.Adapter;

namespace ExamInt.App.Api.Adapter
{
    /// <summary>
    /// Permission Adapter
    /// </summary>
    public class PermissionAdapter : ConsultAdapterBase<PermissionM, Permission, PermissionListedItemM, PermissionListedItem, PermissionListedCriteriaM, PermissionListedCriteria, IPermissionLogic>, IPermissionAdapter
    {
    }
}

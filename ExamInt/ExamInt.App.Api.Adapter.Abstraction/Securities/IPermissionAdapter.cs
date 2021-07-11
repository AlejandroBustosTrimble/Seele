using ExamInt.App.Api.Model;
using RAK.Core.Api.Adapter.Abstraction;

namespace ExamInt.App.Api.Adapter.Abstraction
{
    /// <summary>
    /// Permission adapter interface
    /// </summary>
    public interface IPermissionAdapter : IConsultAdapter<PermissionM, PermissionListedItemM, PermissionListedCriteriaM>
    {
    }
}

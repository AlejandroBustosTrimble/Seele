using ExamInt.App.Api.Adapter.Abstraction;
using ExamInt.App.Api.Model;
using RAK.Core.Api.WebApi;

namespace ExamInt.App.Api.WebApi.Controllers
{
    /// <summary>
    /// Permission Controller
    /// </summary>
    public class PermissionController : ConsultWebApiControllerBase<PermissionM, PermissionListedItemM, PermissionListedCriteriaM, IPermissionAdapter>
    {
    }
}
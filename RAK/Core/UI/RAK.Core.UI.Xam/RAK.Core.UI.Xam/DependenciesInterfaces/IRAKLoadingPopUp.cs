using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.DependenciesInterfaces
{
    /// <summary>
    /// Interface para dependencia de PopUps
    /// </summary>
    public interface IRAKLoadingPopUp
    {
        Task<bool> ShowPopUp();
        Task<bool> ClosePopUp();
    }
}

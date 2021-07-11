using System.Threading.Tasks;

namespace RAK.Core.UI.Xam
{
    public interface IIdentityValidation
    {
        Task<IdentityValidationResponse> ValidateIdentity();
    }

    public class IdentityValidationResponse
    {
        public string Message { get; set; }
        public bool Result { get; set; }
    }
}

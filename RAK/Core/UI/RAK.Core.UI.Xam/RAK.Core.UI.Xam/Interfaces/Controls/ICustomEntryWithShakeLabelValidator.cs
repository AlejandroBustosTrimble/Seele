
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Interfaces
{

    /// <summary>
    /// Validador para CustomEntryWithShakeLabel
    /// </summary>
    public interface ICustomEntryWithShakeLabelValidator
    {
        CustomEntryWithShakeLabelValidateResult Validate(string Text);
    }

    /// <summary>
    /// Respuesta de Validador
    /// </summary>
    public class CustomEntryWithShakeLabelValidateResult
    {
        public ValidateResultType ResultType { get; set; }
        public string Message { get; set; }
        public Color? LabelColorToShowMessage { get; set; }

        /// <summary>
        /// Indica si la respuesta es exito
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return ResultType == ValidateResultType.Success;
            }
        }

        /// <summary>
        /// Indica si la respuesta es error
        /// </summary>
        public bool IsError
        {
            get
            {
                return ResultType == ValidateResultType.Error;
            }
        }

        /// <summary>
        /// Indica si la respuesta es warning
        /// </summary>
        public bool IsWarning
        {
            get
            {
                return ResultType == ValidateResultType.Warning;
            }
        }

        /// <summary>
        /// Retorna una nueva Instancia de Resultado
        /// </summary>
        public static CustomEntryWithShakeLabelValidateResult CreateInstance()
        {
            return new CustomEntryWithShakeLabelValidateResult();
        }
    }

    /// <summary>
    /// Tipos de Resultados
    /// </summary>
    public enum ValidateResultType
    {
        Success = 0,
        Warning = 1,
        Error = 2
    }

}

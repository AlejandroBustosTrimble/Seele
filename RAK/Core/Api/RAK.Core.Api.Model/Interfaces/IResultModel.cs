namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// ViewModel de resultado
    /// </summary>
    public interface IResultModel : IModelBase, IResponseModel
    {
        /// <summary>
        /// Obtiene si la operacion fue exitosa o no
        /// </summary>
        bool IsSuccess { get; set; }

        /// <summary>
        /// Obtienes la cantidad de entidades afectadas
        /// </summary>
        int Count { get; set; }
    }
}

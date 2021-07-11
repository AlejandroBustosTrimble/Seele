namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// ViewModel utilizado para obtener una lista de viewModels
    /// </summary>
    public interface IGetListModel : IModelBase
    {
        /// <summary>
        /// Cantidad de elementos a traer
        /// </summary>
        int? Count { get; set; }

        /// <summary>
        /// Cantidad de elementos a descartar desde el pricipio
        /// </summary>
        int? OffSet { get; set; }
    }
}

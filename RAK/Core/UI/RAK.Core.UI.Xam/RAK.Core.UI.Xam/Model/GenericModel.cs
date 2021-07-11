namespace RAK.Core.UI.Xam.Model
{
    /// <summary>
    /// Base clase para modelos
    /// </summary>
    public abstract class GenericModel<Req, Res> : IGenericModel<Req, Res>
    where Req : IRequestXam
    where Res : IResponseXam
    {
        /// <summary>
        /// Submit del Modelo
        /// </summary>
        public abstract IResponseXamPackage<Res> Submit(Req Req);
    }

    /// <summary>
    /// Base clase para modelos
    /// </summary>
    public abstract class GenericModelList <Req, Res> : IGenericListModel<Req, Res>
    where Req : IRequestXam
    where Res : IResponseXam
    {
        /// <summary>
        /// Submit del Modelo
        /// </summary>
        public abstract IResponseXamPackageList<Res> Submit(Req Req);
    }

    public interface IGenericModel<Req, Res>
    where Req : IRequestXam
    where Res : IResponseXam
    {
        IResponseXamPackage<Res> Submit(Req Req);
    }

    public interface IGenericListModel<Req, Res>
    where Req : IRequestXam
    where Res : IResponseXam
    {
        IResponseXamPackageList<Res> Submit(Req Req);
    }

}

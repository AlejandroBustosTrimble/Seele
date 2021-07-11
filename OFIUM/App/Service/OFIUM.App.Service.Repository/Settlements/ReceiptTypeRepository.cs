using OFIUM.App.Service.Entity;
using OFIUM.App.Service.Repository.Abstraction;
using RAK.Core.Service.Entity;
using RAK.Core.Service.Repository;

namespace OFIUM.App.Service.Repository
{
    /// <summary>
    /// Repositorio de Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptTypeRepository : ConsultRepositoryBase<ReceiptType, ReceiptTypeListedItem, ReceiptTypeListedCriteria>, IReceiptTypeRepository
    {
        #region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        public ReceiptTypeRepository():base()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene una lista de entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ListEntity<ReceiptTypeListedItem> GetListed(ReceiptTypeListedCriteria entity)
        {
            //// TODO_RAK:
            var tempList = this.GetAll(new GetListEntity());
            var list = new ListEntity<ReceiptTypeListedItem>();

            foreach(var item in tempList.List)
            {
                list.List.Add(new ReceiptTypeListedItem()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Sign = item.Sign
                });
            }

            return list;
        }

        #endregion
    }
}

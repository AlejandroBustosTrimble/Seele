using RAK.Fwk.Common.AbstractEntity;
using System;

namespace OFIUM.App.Common.AbstractEntity
{
    /// <summary>
    /// Interfaz de la entidad Item de Lista de Tipo de Factura
    /// </summary>
    public interface IReceiptTypeListedItem : IEntityGeneric
    {
        Decimal Sign { get; set; }
    }
}

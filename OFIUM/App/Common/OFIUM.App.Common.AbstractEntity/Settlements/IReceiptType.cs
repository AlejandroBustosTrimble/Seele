using RAK.Fwk.Common.AbstractEntity;
using System;

namespace OFIUM.App.Common.AbstractEntity
{
    /// <summary>
    /// Interfaz de Tipo de comprobante (Factura A, etc)
    /// </summary>
    public interface IReceiptType : IEntityWithNameGeneric
    {
        Decimal Sign { get; set; }
    }
}

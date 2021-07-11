using System;

namespace RAK.Fwk.Common.AbstractEntity.Attributes
{
    /// <summary>
    /// Atributo que indica que una Propiedad es Identificadora (ID) de una Entidad.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DataKeyAttribute : Attribute
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Interfaces
{
    // TODO_RAK: a las entidades abstract entities posta las voy a marcar con atributos de validaciones
    // como ser si es requerido, o que cumpla tal patron como mail o un numero de tal tipo
    // Para que por un lado cuando haga los helpers de controles UI renderice el JS referido a esto
    // y por otro lado en la logica, va a llegar la entidad que implementa la misma abstractEntity
    // Por lo que voy a obtener lso atributos asociados a esa interfaz y realizar validaciones
    // Esto me va a automatizar lo mas que pueda las validaciones tanto de lado del cliente como de la logica
    // Si necesito validaciones mas complejas voy a hacerlas del lado de Logica y/o por codigo JS

    /// <summary>
    /// Interfaz que marca los Modelos que son Requests
    /// </summary>
    public interface IRequestModel
    {
    }
}

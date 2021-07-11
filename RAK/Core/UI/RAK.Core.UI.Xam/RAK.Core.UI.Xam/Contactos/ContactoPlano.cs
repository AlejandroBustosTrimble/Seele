using RAK.Core.UI.Xam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Contactos
{
    public class ContactoPlano : ModelBase
    {
        public string ContactID { get; set; }
        public string IMG { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public long AccountID { get; set; }
        public long AccountTypeID { get; set; }
        public string AccountName { get; set; }
        public string AccountImage { get; set; }
        public string DatoContacto
        {
            get
            {
                if (!string.IsNullOrEmpty(Telefono))
                    return Telefono;
                else if (!string.IsNullOrEmpty(Email))
                    return Email;
                else return string.Empty;
            }
        }
    }
}

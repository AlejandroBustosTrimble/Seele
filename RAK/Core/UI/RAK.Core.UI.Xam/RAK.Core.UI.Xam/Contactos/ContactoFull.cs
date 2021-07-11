using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Contactos
{
    public class ContactoFull
    {
        public string ContactID { get; set; }
        public string Name { get; set; }
        public string PhotoUri { get; set; }
        public string PhotoUriThumbnail { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }

        public List<string> Numbers { get; set; }
        public List<string> Emails { get; set; }

        public ContactoFull()
        {
            Numbers = new List<string>();
            Emails = new List<string>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

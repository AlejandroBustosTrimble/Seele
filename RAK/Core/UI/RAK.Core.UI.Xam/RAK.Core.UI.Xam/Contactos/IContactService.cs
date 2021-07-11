using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.Contactos
{
    public interface IContactService
    {
        Task<IList<ContactoFull>> GetContactListAsync();
        IList<ContactoFull> GetContactList();
    }
}

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Contactos
{
    public static class ContactosHelper
    {
        /// <summary>
        /// Retorna 1 registro por cada dato que tiene el contacto para buscar por posibles multiples cuentas
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ContactoPlano>> GetContactsPlano()
        {
            var contactos = new List<ContactoPlano>();
            var positionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);

            if (positionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Contacts });
                positionStatus = results[Permission.Contacts];

                if (positionStatus != PermissionStatus.Granted)
                     return contactos; 
            }

            try
            {
                var contacts = await DependencyService.Get<IContactService>().GetContactListAsync();

                foreach (var contact in contacts)
                {

                    foreach (var item in contact.Emails)
                    {
                        contactos.Add(new ContactoPlano() { Email = item, Nombre = contact.Name, ContactID = contact.ContactID });
                    }

                    foreach (var item in contact.Numbers)
                    {
                        contactos.Add(new ContactoPlano() { Telefono = item, Nombre = contact.Name, ContactID = contact.ContactID });
                    }
                }

                return contactos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Retorna un contacto con todos sus telefonos e emails
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ContactoFull>> GetContacts()
        {

            var positionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);

            if (positionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Contacts });
                positionStatus = results[Permission.Contacts];

                if (positionStatus != PermissionStatus.Granted)
                    return new List<ContactoFull>();
            }

            try
            {
                var contactos = await DependencyService.Get<IContactService>().GetContactListAsync();
                return contactos.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

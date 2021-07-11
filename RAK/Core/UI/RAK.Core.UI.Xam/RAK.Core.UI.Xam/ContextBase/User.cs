using RAK.Core.UI.Xam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.ContextBase
{
    public abstract class BasicXamarinAppUser : ModelBase, IXamarinAppUser
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long ProfileID { get; set; }
    }

    public interface IXamarinAppUser
    {
        string Name { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        long ProfileID { get; set; }
    }
}
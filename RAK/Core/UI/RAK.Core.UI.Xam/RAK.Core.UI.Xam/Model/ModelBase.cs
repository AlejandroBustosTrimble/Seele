using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Model
{
    public class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public long LocalID { get; set; }
        
        public long EntityID { get; set; }
    }
}

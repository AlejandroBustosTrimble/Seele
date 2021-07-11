using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// ViewModel base
    /// </summary>
    public abstract class ModelBase : IModelBase
    {

        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
    }
}

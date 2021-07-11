using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.Api.Model
{
    public class ModelWithNameBase : ModelBase, IModelWithNameBase
    {
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }
    }
}

using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// Criteria de una vista basica
    /// </summary>
    public class BasicCriteriaModel : GetListModel, IBasicCriteriaModel, IRequestModel
    {
        public String Text { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using OFIUM.App.Api.Adapter.Abstraction;
using OFIUM.App.Api.Model;
using OFIUM.App.Api.Model.Interfaces;
using RAK.Core.Api.CoreApi;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using RAK.Fwk.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFIUM.App.Api.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Controller Api de Tipo de Factura
    /// </summary>
    public class InvoiceTypeController : ConsultWebApiControllerBase<InvoiceTypeM, InvoiceTypeListedItemM, InvoiceTypeListedCriteriaM, IInvoiceTypeAdapter>
    {
        public InvoiceTypeController()
        {
            //var coreModelAssembly = typeof(RAK.Core.Api.Model.ListedItemModel).Assembly;

            //DIEngineContainer.Instance.RegisterGeneric(typeof(IListModel<>), typeof(ListModel<>));

            ////DIEngineContainer.Instance.RegisterAssemblyWithGenerics(typeof(IListModel<>), coreModelAssembly);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };



        }
    }
}
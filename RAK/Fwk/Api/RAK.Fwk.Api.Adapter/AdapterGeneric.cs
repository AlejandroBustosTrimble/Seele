using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Service.Logic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Fwk.Api.Adapter
{
    /// <summary>
    /// Adapter generico
    /// </summary>
    /// <typeparam name="L"></typeparam>
    public abstract class AdapterGeneric<L> : IAdapterGeneric
        where L : ILogicGeneric
    {
        #region Members

        private L logic;

        #endregion

        /// <summary>
        /// Logica
        /// </summary>
        protected L Logic
        {
            get
            {
                if (this.logic == null)
                {
                    this.logic = DIEngineContainer.Instance.Resolve<L>();
                }

                return this.logic;
            }
        }
    }
}

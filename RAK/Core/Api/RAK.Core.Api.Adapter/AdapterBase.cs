using RAK.Core.Api.Model.Interfaces;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Fwk.Api.Adapter;
using RAK.Fwk.Common.DependencyInjection;
using RAK.Fwk.Common.Mapper;
using RAK.Fwk.Service.Logic.Abstraction;
using System;

namespace RAK.Core.Api.Adapter
{
    /// <summary>
    /// Adapter Base
    /// Es el traductor entre Model y Entity
    /// </summary>
    /// <typeparam name="L"></typeparam>
    public abstract class AdapterBase<L> : AdapterGeneric<L>
        where L : ILogicGeneric
    {
        /// <summary>
        /// Ejecuta una accion
        /// </summary>
        /// <typeparam name="REQMO"></typeparam>
        /// <typeparam name="RESMO"></typeparam>
        /// <typeparam name="REQEN"></typeparam>
        /// <typeparam name="RESEN"></typeparam>
        /// <param name="funcToRun"></param>
        /// <param name="reqVM"></param>
        /// <returns></returns>
        protected RESMO ExecuteAction<REQMO, RESMO, REQEN, RESEN>(Func<REQEN, RESEN> funcToRun, REQMO reqVM)
            where REQMO : IModelBase
            where RESMO : IModelBase, new()
            where REQEN : IEntityBase
            where RESEN : IEntityBase
        {
            var response = new RESMO();

            try
            {
                // TODO_RAK: no hacer new, Registrar el Mapper con DI, asi puedo segun el caso pisar el mapper que uso
                // por ej para uno que herede de este mapper y que tengan en cuenta Nhibernate porque tengo que cambiar el 
                // mapper en este caso
                var mapper = new Mapper();
                var reqEntity = mapper.Map<REQMO, REQEN>(reqVM);

                var resEntity = funcToRun(reqEntity);

                response = mapper.Map<RESEN, RESMO>(resEntity);
            }
            catch (Exception ex)
            {
                // TODO_RAK: Si lo estuviera llamando en una aplicacion de escritorio o algo que corre en un dispositivo
                // Por ej donde termine accediendo a un SQLite local, no voy a llamar a un api, sino que voy a llamar
                // Directo a Adapter, por lo que en este caso deberia meter logs aca, poner algun tipo de boolean
                // Por defecto en false para que si hace falta lo pongo en True y solamente en estos casos meto logs

                //LoggerContainer.Instance.AddErrorLog(ex);
                throw;
            }

            return response;
        }
    }
}

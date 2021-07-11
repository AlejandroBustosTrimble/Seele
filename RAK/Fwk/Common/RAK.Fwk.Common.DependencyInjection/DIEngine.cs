using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace RAK.Fwk.Common.DependencyInjection
{
    /// <summary>
    /// Motor de inyeccion de dependencias
    /// </summary>
    public class DIEngine
    {
        #region Constructor

        /// <summary>
        /// Cttor
        /// </summary>
        internal DIEngine()
        { }

        #endregion

        #region Static

        private IContainer container;

        /// <summary>
        /// Contenedor
        /// </summary>
        internal IContainer Container
        {
            get
            {
                return this.container;
            }
            private set
            {
                this.container = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Obtiene si existe el contenedor DI
        /// </summary>
        /// <returns></returns>
        public bool ContainerExists
        {
            get
            {
                return this.Container != null;
            }
        }

        #endregion

        #region Methods

        #region Registration types

        /// <summary>
        /// Registra un tipo
        /// </summary>
        /// <typeparam name="TFrom">Tipo a implementar(Interfaz, clase base, etc)</typeparam>
        /// <typeparam name="TTo">Tipo concreto</typeparam>
        public void Register<TFrom, TTo>()
            where TTo : TFrom
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TTo>().As<TFrom>();

            this.UpdateContainer(builder);
        }

        /// <summary>
        /// Registra un tipo
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="typeTo"></param>
        public void Register<TFrom>(Type typeTo)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeTo).As<TFrom>();

            this.UpdateContainer(builder);
        }

        /// <summary>
        /// Registra un tipo
        /// </summary>
        /// <typeparam name="TFrom">Tipo a implementar(Interfaz, clase base, etc)</typeparam>
        /// <typeparam name="TTo">Tipo concreto</typeparam>
        public void RegisterGeneric(Type typeFrom, Type typeTo)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeTo)
            .As(typeFrom);

            this.UpdateContainer(builder);
        }

        /// <summary>
        /// Registra un assembly, todas aquellas clases que implementen determinado tipo
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly<TImplement>(String assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);

            this.RegisterAssembly<TImplement>(assembly);
        }

        /// <summary>
        /// Registra varios assemblies, todas aquellas clases que implementen determinado tipo
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly<TImplement>(params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblies)
                  .Where(t => t.GetInterfaces()
                    .Any(i => i.IsAssignableFrom(typeof(TImplement))))
                  .AsImplementedInterfaces()
                  .InstancePerDependency();

            this.UpdateContainer(builder);
        }

        /// <summary>
        /// Registra varios assemblies, todas aquellas clases que implementen determinado tipo
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly<TImplement>(Enum key, params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblies)
                  .Where(t => t.GetInterfaces()
                    .Any(i => i.IsAssignableFrom(typeof(TImplement))))
                  .AsImplementedInterfaces()
                  .Keyed<TImplement>(key)
                  .InstancePerDependency();

            this.UpdateContainer(builder);
        }

        /// <summary>
        /// Registra varios assemblies, todas aquellas clases que implementen determinado tipo
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly<TImplement>(String key, params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblies)
                  .Where(t => t.GetInterfaces()
                    .Any(i => i.IsAssignableFrom(typeof(TImplement))))
                  .AsImplementedInterfaces()
                  .Named<TImplement>(key)
                  .InstancePerDependency();

            this.UpdateContainer(builder);
        }

        #endregion

        #region Resolve Types

        /// <summary>
        /// Resuelve un tipo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return this.Container.Resolve<T>();
        }

        /// <summary>
        /// Resuelve un tipo
        /// </summary>
        /// <param name="typeFrom"></param>
        /// <returns></returns>
        public Object Resolve(Type typeFrom)
        {
            return this.Container.Resolve(typeFrom);
        }

        #endregion

        #region Private

        /// <summary>
        /// Actualiza el contenedor
        /// </summary>
        /// <param name="builder"></param>
        private void UpdateContainer(ContainerBuilder builder)
        {
            if(this.ContainerExists)
            {
                builder.Update(this.Container);
            }
            else
            {
                this.Container = builder.Build();
            }
        }

        #endregion

        #endregion
    }
}

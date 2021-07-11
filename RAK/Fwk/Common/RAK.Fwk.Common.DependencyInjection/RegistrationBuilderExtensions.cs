using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Fwk.Common.DependencyInjection
{
    /// <summary>
    /// Extension methods for the <see cref="RegistrationBuilder{TLimit,TActivatorData,TRegistrationStyle}"/> class.
    /// </summary>
    internal static class RegistrationBuilderExtensions
    {
        /// <summary>
        /// Specifies that a type from a scanned assembly is registered if it implements an interface
        /// that closes the provided open generic interface type.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <typeparam name="TScanningActivatorData">Activator data type.</typeparam>
        /// <param name="registration">Registration to set service mapping on.</param>
        /// <param name="openGenericInterfaceType">The open generic interface type for which implementations will be found.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle>
            WhereTypeClosesOpenGenericInterface<TLimit, TScanningActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> registration, Type openGenericInterfaceType)
            where TScanningActivatorData : ScanningActivatorData
        {
            if (openGenericInterfaceType == null)
            {
                throw new ArgumentNullException("openGenericInterfaceType");
            }

            if (!(openGenericInterfaceType.IsGenericTypeDefinition || openGenericInterfaceType.ContainsGenericParameters) || !openGenericInterfaceType.IsInterface)
            {
                throw new ArgumentException("The type '" + openGenericInterfaceType.FullName + "' is not an open generic interface type.");
            }

            return registration.Where(candidateType => findInterfaceThatCloses(candidateType, openGenericInterfaceType) != null)
                .As(candidateType => findInterfaceThatCloses(candidateType, openGenericInterfaceType));
        }

        /// <summary>
        /// Looks for an interface on the candidate type that closes the provided open generic interface type.
        /// </summary>
        /// <param name="candidateType">The type that is being checked for the interface.</param>
        /// <param name="openGenericInterfaceType">The open generic interface type to locate.</param>
        /// <returns>The type of the interface if found; otherwise, <c>null</c>.</returns>
        private static Type findInterfaceThatCloses(Type candidateType, Type openGenericInterfaceType)
        {
            if (candidateType.IsAbstract) return null;

            foreach (Type interfaceType in candidateType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGenericInterfaceType)
                {
                    return interfaceType;
                }
            }

            return (candidateType.BaseType == typeof(object))
                ? null
                : findInterfaceThatCloses(candidateType.BaseType, openGenericInterfaceType);
        }
    }
}

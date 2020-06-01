using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamSample.AppHelper
{
    /// <summary>
    /// Defines the <see cref="IocContainer" />.
    /// </summary>
    public static class IocContainer
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the Instances.
        /// </summary>
        private static readonly IList<Type> Instances = new List<Type>();

        /// <summary>
        /// Defines the staticInstances.
        /// </summary>
        private static readonly IDictionary<Type, object> staticInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Defines the typeInstances.
        /// </summary>
        private static readonly IDictionary<Type, object> typeInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Defines the types.
        /// </summary>
        private static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The Register.
        /// </summary>
        /// <typeparam name="TContract">.</typeparam>
        /// <typeparam name="TImplementation">.</typeparam>
        public static void Register<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }

        /// <summary>
        /// The Register.
        /// </summary>
        /// <typeparam name="TImplementation">.</typeparam>
        public static void Register<TImplementation>()
        {
            Instances.Add(typeof(TImplementation));
        }

        /// <summary>
        /// The Register.
        /// </summary>
        /// <typeparam name="TContract">.</typeparam>
        /// <typeparam name="TImplementation">.</typeparam>
        /// <param name="instance">The instance<see cref="TImplementation"/>.</param>
        public static void Register<TContract, TImplementation>(TImplementation instance)
        {
            typeInstances[typeof(TContract)] = instance;
        }

        /// <summary>
        /// The RegisterInstance.
        /// </summary>
        /// <param name="instance">The instance<see cref="object"/>.</param>
        public static void RegisterInstance(object instance)
        {
            staticInstances[instance.GetType()] = instance;
        }

        /// <summary>
        /// The RegisterSinglton.
        /// </summary>
        /// <typeparam name="TContract">.</typeparam>
        /// <typeparam name="TImplementation">.</typeparam>
        public static void RegisterSinglton<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
            var result = Resolve<TContract>();
            typeInstances[typeof(TContract)] = result;
        }

        /// <summary>
        /// The Resolve.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// The Resolve.
        /// </summary>
        /// <param name="contract">The contract<see cref="Type"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static object Resolve(Type contract)
        {
            if (typeInstances.ContainsKey(contract))
            {
                return typeInstances[contract];
            }
            else if (staticInstances.ContainsKey(contract))
            {
                return staticInstances[contract];
            }
            else
            {
                Type implementation = Instances.Contains(contract) ? contract : types[contract];
                ConstructorInfo constructor = implementation.GetConstructors()[0];
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }
                return constructor.Invoke(parameters.ToArray());
            }
        }

        #endregion
    }
}

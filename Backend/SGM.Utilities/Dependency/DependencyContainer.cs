using System;
using System.Collections;

namespace Orion.Utilities.Dependency {
    /// <summary>
    /// A static class that aims to serve as an option to DI.
    /// Its behavior is similar to a factory, where an instance of a concrete class can be obtained just by passing its type, supertype, or interface.
    /// The typical use-case is, when registering a concrete type, the interface which said type implements is passed as well. Once that is done, an instance of this concrete class can be obtained through its interface.
    /// This is to reduce class coupling, allowing developers to avoid newing up instances of a class (especially services) being consumed.
    /// </summary>
    public static class DependencyContainer {
        private static Hashtable types;

        static DependencyContainer() {
            types = new Hashtable();
        }

        /// <summary>
        /// Registers a type into the container.
        /// </summary>
        /// <typeparam name="TInterface">An interface implemented by TConcrete, through which instances of TConcrete will be obtained.</typeparam>
        /// <typeparam name="TConcrete">A concrete type that will be returned when an instance of TInterface is requested.</typeparam>
        public static void Register<TInterface, TConcrete>() where TConcrete : class, new() {
            if (typeof(TConcrete).GetInterface(typeof(TInterface).ToString()) != typeof(TInterface) && typeof(TInterface) != typeof(TConcrete))
                throw new Exception($"ERROR: Type {typeof(TConcrete)} is not an implementation of {typeof(TInterface)}.");

            if (!typeof(TInterface).IsInterface && typeof(TInterface) != typeof(TConcrete))
                throw new Exception($"ERROR: Type {typeof(TInterface)} is not an interface.");

            if (!typeof(TConcrete).IsClass || typeof(TConcrete).IsAbstract)
                throw new Exception($"ERROR: Type {typeof(TConcrete)} is not a concrete class.");

            if (types.ContainsKey(typeof(TInterface)))
                throw new Exception($"ERROR: Type {typeof(TInterface)} has already been registered.");

            types.Add(typeof(TInterface), new TConcrete());
        }

        /// <summary>
        /// Unregisters a type from the container.
        /// </summary>
        /// <typeparam name="TInterface">The interface of the type to be unregistered.</typeparam>
        public static void Unregister<TInterface>() {
            if (!types.ContainsKey(typeof(TInterface)))
                throw new Exception($"ERROR: Cannot unregister type {typeof(TInterface)}. Type not found. ");

            types.Remove(typeof(TInterface));
        }

        /// <summary>
        /// Returns an instance of the concrete class registered for the specified interface TInterface.
        /// By design, this method will always return the same instance (singleton behavior) of the registered concrete type.
        /// </summary>
        /// <typeparam name="TInterface">The interface registered for the desired returning object's type.</typeparam>
        /// <returns>An instance of the concrete type registered for TInterface.</returns>
        public static TInterface GetInstance<TInterface>() where TInterface : class {
            if (!types.ContainsKey(typeof(TInterface)))
                throw new Exception($"ERROR: No concrete type assigned to interface {typeof(TInterface)}");

            return types[typeof(TInterface)] as TInterface;
        }

        /// <summary>
        /// Returns an instance of the concrete class registered for the specified interface TInterface.
        /// This method will return a new instance of the registered concrete type.
        /// </summary>
        /// <typeparam name="TInterface">The interface registered for the desired returning object's type.</typeparam>
        /// <returns>An instance of the concrete type registered for TInterface.</returns>
        public static TInterface GetInstanceAsNew<TInterface>() where TInterface : class {
            if (!types.ContainsKey(typeof(TInterface)))
                throw new Exception($"ERROR: No concrete type assigned to interface {typeof(TInterface)}");

            var type = types[typeof(TInterface)].GetType();

            return Activator.CreateInstance(type) as TInterface;
        }
    }
}

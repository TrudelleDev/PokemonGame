using System;
using System.Collections.Generic;

namespace PokemonGame
{
    /// <summary>
    /// Lightweight global service locator for game systems.
    /// Provides centralized registration and retrieval of services.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new();

        /// <summary>
        /// Registers a service instance of type <typeparamref name="T"/>.
        /// </summary>
        public static void Register<T>(T service) where T : class
        {
            if (service == null)
            {
                Log.Warning(nameof(ServiceLocator), $"Tried to register a null service of type {typeof(T).Name}.");
                return;
            }

            if (services.ContainsKey(typeof(T)))
            {
                Log.Warning(nameof(ServiceLocator), $"Service of type {typeof(T).Name} already registered. Overwriting.");
            }

            services[typeof(T)] = service;
        }

        /// <summary>
        /// Gets the registered service of type <typeparamref name="T"/>.
        /// Returns null if not found.
        /// </summary>
        public static T Get<T>() where T : class
        {
            if (services.TryGetValue(typeof(T), out var svc))
            {
                return svc as T;
            }

            Log.Warning(nameof(ServiceLocator), $"Requested service of type {typeof(T).Name} is not registered.");
            return null;
        }

        /// <summary>
        /// Unregisters a service of type <typeparamref name="T"/>.
        /// </summary>
        public static void Unregister<T>() where T : class
        {
            services.Remove(typeof(T));
        }

        /// <summary>
        /// Clears all registered services (useful for scene reloads).
        /// </summary>
        public static void Clear()
        {
            services.Clear();
        }
    }
}

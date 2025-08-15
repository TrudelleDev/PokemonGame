using System;
using System.Collections.Generic;

namespace PokemonGame
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new();

        public static void Register<T>(T service) where T : class
        {
            services[typeof(T)] = service;
        }

        public static T Get<T>() where T : class
        {
            if (services.TryGetValue(typeof(T), out var svc))
            {
                return svc as T;
            }
            return null;
        }

        public static void Unregister<T>() where T : class
        {
            services.Remove(typeof(T));
        }
    }
}

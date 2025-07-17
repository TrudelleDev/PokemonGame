using UnityEngine;

namespace PokemonGame
{
    public static class Log
    {
        public static void Info(object context, string message)
        {
            Debug.Log(Format(context, message));
        }

        public static void Warning(object context, string message)
        {
            Debug.LogWarning(Format(context, message));
        }

        public static void Error(object context, string message)
        {
            Debug.LogError(Format(context, message));
        }

        private static string Format(object context, string message)
        {
            string label = context != null ? $"[{context.GetType().Name}]" : "[Unknown]";
            return $"{label} {message}";
        }
    }
}

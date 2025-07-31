using System.Diagnostics;

namespace PokemonGame
{
    public static class Log
    {
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Info(string tag, string message)
        {
            UnityEngine.Debug.Log(Format(tag, message));
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Warning(string tag, string message)
        {
            UnityEngine.Debug.LogWarning(Format(tag, message));
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Error(string tag, string message)
        {
            UnityEngine.Debug.LogError(Format(tag, message));
        }

        private static string Format(string tag, string message)
        {
            string label = string.IsNullOrWhiteSpace(tag) ? "[Unknown]" : $"[{tag}]";
            return $"{label} {message}";
        }
    }
}

using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Generic base class for implementing a singleton MonoBehaviour.
    /// Ensures only one instance exists and persists across scene loads.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Log.Warning(nameof(Singleton<T>), $"Duplicate {typeof(T).Name} detected, destroying.");
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            //DontDestroyOnLoad(gameObject);
        }
    }
}

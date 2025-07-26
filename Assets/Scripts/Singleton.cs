using UnityEngine;

namespace PokemonGame
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"[Singleton<{typeof(T).Name}>] Duplicate detected, destroying.");
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            //DontDestroyOnLoad(gameObject); // Optional
        }
    }
}

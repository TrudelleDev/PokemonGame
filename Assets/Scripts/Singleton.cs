using UnityEngine;

namespace PokemonGame
{
    public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }
        }
    }
}

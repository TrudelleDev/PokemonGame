using System;
using UnityEngine;

namespace MonsterTamer.SceneManagement
{
    /// <summary>
    /// Component that signals when its scene is ready for use.
    /// Useful for systems (e.g. <see cref="PlayerRelocator"/>) 
    /// that need to react after initialization without relying 
    /// directly on Unity's SceneManager callbacks.
    /// </summary>
    public class SceneReadyNotifier : MonoBehaviour
    {
        /// <summary>
        /// Invoked once during <see cref="Start"/>, after all objects in the scene 
        /// have been initialized and the scene is considered ready.
        /// </summary>
        public static event Action OnSceneReady;

        private void Start()
        {
            OnSceneReady?.Invoke();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace PokemonGame
{
    /// <summary>
    /// Ensures a single EventSystem persists across all scenes.
    /// Prevents duplicate EventSystems during scene transitions.
    /// </summary>
    [DefaultExecutionOrder(-100)] // Run early to enforce uniqueness
    public class EventSystemManager : Singleton<EventSystemManager>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            // Make sure we have an EventSystem + InputModule
            if (GetComponent<EventSystem>() == null)
            {
                gameObject.AddComponent<EventSystem>();
            }
        }
    }
}

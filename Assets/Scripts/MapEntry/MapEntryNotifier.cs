using UnityEngine;

namespace PokemonGame.MapEntry
{
    /// <summary>
    /// Notifies the MapEntryRegistry when this map has finished loading,
    /// so the player can be placed at the correct entry point.
    /// </summary>
    public class MapEntryNotifier : MonoBehaviour
    {
        private void Start()
        {
            // Signal that all entry points in this map are now registered
            MapEntryRegistry.NotifyEntryPointsReady();
        }
    }
}

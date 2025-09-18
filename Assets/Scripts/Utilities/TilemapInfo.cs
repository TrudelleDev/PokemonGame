using UnityEngine;

namespace PokemonGame.Utilities
{
    /// <summary>
    /// Provides global access to the tile grid cell size.
    /// Reads from the attached Grid when the scene loads.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Grid))]
    [ExecuteAlways]
    public class TilemapInfo : MonoBehaviour
    {
        public static Vector3 CellSize { get; private set; }

        private void OnEnable()
        {
            // Note: use OnEnable (not Awake) for editor-time updates
            CellSize = GetComponent<Grid>().cellSize;
        }
    }
}

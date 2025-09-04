using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Provides global access to the tile grid cell size.
    /// Reads from the attached Grid when the scene loads.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Grid))]
    public class TilemapInfo : MonoBehaviour
    {
        public static Vector3 CellSize { get; private set; }

        private void Awake()
        {
            CellSize = GetComponent<Grid>().cellSize;
        }
    }
}

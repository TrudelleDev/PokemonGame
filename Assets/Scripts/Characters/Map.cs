using UnityEngine;

namespace PokemonGame
{
    public class Map : MonoBehaviour
    {
        private void Awake()
        {
            Grid grid = GetComponentInParent<Grid>();
            TilemapInfo.CellSize = grid.cellSize;
        }
    }
}

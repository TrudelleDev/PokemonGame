using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Restricts an NPC's movement within a rectangular tile area
    /// relative to its spawn/start position.
    /// </summary>
    [DisallowMultipleComponent]
    public class NpcMovementBounds : MonoBehaviour
    {
        [Header("Tile Limits (relative to origin tile)")]

        [SerializeField, Tooltip("Minimum X offset from the spawn tile.")]
        private int minX = -1;

        [SerializeField, Tooltip("Maximum X offset from the spawn tile.")]
        private int maxX = 1;

        [SerializeField, Tooltip("Minimum Y offset from the spawn tile.")]
        private int minY = -1;

        [SerializeField, Tooltip("Maximum Y offset from the spawn tile.")]
        private int maxY = 1;

        private Vector2Int originTile;

        private void Awake()
        {
            // Cache starting tile as the origin
            originTile = WorldToTile(transform.position);
        }

        /// <summary>
        /// The tile the character currently occupies.
        /// </summary>
        public Vector2Int CurrentTile => WorldToTile(transform.position);

        /// <summary>
        /// Returns true if moving in the given direction would remain inside bounds.
        /// </summary>
        public bool CanMove(Vector2Int direction)
        {
            Vector2Int targetTile = CurrentTile + direction;

            return targetTile.x >= originTile.x + minX &&
                   targetTile.x <= originTile.x + maxX &&
                   targetTile.y >= originTile.y + minY &&
                   targetTile.y <= originTile.y + maxY;
        }

        /// <summary>
        /// Converts a world position into tile coordinates, accounting for cell size.
        /// </summary>
        private Vector2Int WorldToTile(Vector3 worldPosition)
        {
            Vector3 tileCoords = new(
                worldPosition.x / TilemapInfo.CellSize.x,
                worldPosition.y / TilemapInfo.CellSize.y,
                0f
            );

            return new Vector2Int(
                Mathf.RoundToInt(tileCoords.x),
                Mathf.RoundToInt(tileCoords.y)
            );
        }
    }
}

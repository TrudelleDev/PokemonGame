using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Restricts a character's movement within a defined tile box,
    /// relative to the character's spawn/start position.
    /// </summary>
    public class CharacterMovementBounds : MonoBehaviour
    {
        [Header("Tile Limits (relative to spawn)")]

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
        /// Checks if moving in the given direction stays inside the bounds box.
        /// </summary>
        public bool TryMove(Vector2Int direction)
        {
            Vector2Int newTile = CurrentTile + direction;

            return !(newTile.x < originTile.x + minX || newTile.x > originTile.x + maxX ||
                     newTile.y < originTile.y + minY || newTile.y > originTile.y + maxY);
        }

        private Vector2Int WorldToTile(Vector3 pos)
        {
            return new Vector2Int(
                Mathf.RoundToInt(pos.x),
                Mathf.RoundToInt(pos.y)
            );
        }
    }
}

using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PokemonGame.Tile
{
    /// <summary>
    /// A tile that plays a single animation sequence once, then optionally clears itself.
    /// Useful for short FX tiles like grass rustle or splash effects.
    /// </summary>
    [CreateAssetMenu(fileName = "NewOneShotAnimatedTile", menuName = "Tiles/One-Shot Animated Tile")]
    public class OneShotAnimatedTile : TileBase
    {
        // Minimum FPS value used to prevent division-by-zero when calculating frame delay.
        private const float MinFpsThreshold = 0.01f;

        // Conversion factor from frames per second to seconds per frame (1 second).
        private const float SecondsPerUnit = 1f;

        // A small delay (in seconds) to ensure the final animation frame renders before clearing the tile.
        private const float FinalFrameClearDelay = 0.02f;

        [Title("Animation")]

        [SerializeField, Required]
        [Tooltip("Sprites used as animation frames, in order.")]
        private Sprite[] frames;

        [SerializeField, MinValue(1)]
        [Tooltip("Animation speed in frames per second.")]
        private float fps = 8f;

        [SerializeField]
        [Tooltip("If true, automatically clears the tile after the animation finishes.")]
        private bool autoClear = false;

        [SerializeField]
        [Tooltip("Collider type applied to each frame.")]
        private UnityEngine.Tilemaps.Tile.ColliderType colliderType = UnityEngine.Tilemaps.Tile.ColliderType.None;

        /// <summary>
        /// Plays this one-shot animation on the given tilemap cell once,
        /// displaying each frame at the configured FPS and optionally clearing afterward.
        /// </summary>
        /// <param name="tilemap">Target <see cref="Tilemap"/> to play the animation on.</param>
        /// <param name="cell">Cell position where the animation occurs.</param>
        /// <returns>Coroutine enumerator for sequential frame playback.</returns>
        public IEnumerator PlayOnce(Tilemap tilemap, Vector3Int cell)
        {
            if (frames == null || frames.Length == 0)
            {
                yield break;
            }

            float frameDelay = SecondsPerUnit / Mathf.Max(fps, MinFpsThreshold);

            for (int i = 0; i < frames.Length; i++)
            {
                var frameTile = CreateInstance<UnityEngine.Tilemaps.Tile>();
                frameTile.sprite = frames[i];
                frameTile.colliderType = colliderType;

                tilemap.SetTile(cell, frameTile);
                tilemap.RefreshTile(cell);

                yield return new WaitForSeconds(frameDelay);
            }

            if (autoClear)
            {
                yield return new WaitForSeconds(FinalFrameClearDelay);
                tilemap.SetTile(cell, null);
                tilemap.RefreshTile(cell);
            }
        }
    }
}

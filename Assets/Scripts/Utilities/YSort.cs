using UnityEngine;

namespace PokemonGame.Utilities
{
    /// <summary>
    /// Dynamically updates the <see cref="SpriteRenderer.sortingOrder"/> 
    /// based on the object's Y position in world space.
    /// Ensures correct draw order for top-down games (e.g. player in front of walls, behind trees).
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class YSort : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        [Tooltip("Optional offset if the sprite's pivot isn't at the feet (e.g., tall objects like trees).")]
        [SerializeField] private int offset = 0;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            // Use Y position to calculate sorting order
            // Multiply by -100 so that 1 Unity unit = 100 sorting steps for precision
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100) + offset;
        }
    }
}

using MonsterTamer.Monster.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Monster.UI
{
    /// <summary>
    /// Displays a monster sprite in the UI based on the configured sprite type.
    /// Automatically hides the image if the sprite is missing or invalid.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    internal sealed class MonsterSprite : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Specifies which monster sprite variant to display.")]
        private MonsterSpriteType spriteType;

        private Image image;

        /// <summary>
        /// Binds a Monster instance and updates the sprite image.
        /// </summary>
        /// <param name="monster">Monster instance to display.</param>
        internal void Bind(MonsterInstance monster)
        {
            EnsureImage();

            if (monster?.Definition?.Sprites == null)
            {
                Unbind();
                return;
            }

            image.sprite = GetSprite(monster);
            image.enabled = image.sprite != null;
        }

        /// <summary>
        /// Hides the sprite image and resets it.
        /// </summary>
        internal void Unbind()
        {
            EnsureImage();
            image.sprite = null;
            image.enabled = false;
        }

        /// <summary>
        /// Returns the sprite matching the configured sprite type.
        /// </summary>
        /// <param name="monster">Monster providing sprite data.</param>
        private Sprite GetSprite(MonsterInstance monster)
        {
            var sprites = monster.Definition.Sprites;

            return spriteType switch
            {
                MonsterSpriteType.Menu => sprites.MenuSprite,
                MonsterSpriteType.Front => sprites.FrontSprite,
                MonsterSpriteType.Back => sprites.BackSprite,
                _ => null
            };
        }

        /// <summary>
        /// Ensures the Image component is cached.
        /// </summary>
        private void EnsureImage()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }
        }
    }
}

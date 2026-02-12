using MonsterTamer.Monster.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Monster.UI
{
    /// <summary>
    /// Displays a monster's primary or secondary type icon.
    /// Automatically hides the icon if the selected type is not available.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    internal sealed class MonsterTypeIcon : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Selects whether to display the primary or secondary monster type.")]
        private MonsterTypeSlot slot;

        private Image typeImage;

        /// <summary>
        /// Binds a monster instance and displays the appropriate type icon.
        /// </summary>
        /// <param name="monster">Monster instance to display.</param>
        internal void Bind(MonsterInstance monster)
        {
            EnsureImage();

            if (monster == null || monster.Definition == null)
            {
                Unbind();
                return;
            }

            Sprite sprite = GetTypeSprite(monster);
            typeImage.sprite = sprite;
            typeImage.gameObject.SetActive(sprite != null);
        }

        /// <summary>
        /// Hides the type icon.
        /// </summary>
        internal void Unbind()
        {
            EnsureImage();
            typeImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite for the configured type slot, or null if unavailable.
        /// </summary>
        /// <param name="monster">Monster providing type data.</param>
        private Sprite GetTypeSprite(MonsterInstance monster)
        {
            var types = monster.Definition.Typing;

            return slot switch
            {
                MonsterTypeSlot.Primary => types.FirstType?.Icon,
                MonsterTypeSlot.Secondary => types.SecondType?.Icon,
                _ => null
            };
        }

        /// <summary>
        /// Ensures the Image component is cached.
        /// </summary>
        private void EnsureImage()
        {
            if (typeImage == null)
            {
                typeImage = GetComponent<Image>();
            }
        }
    }
}

using PokemonGame.Shared.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Shared.UI.MenuButtons
{
    /// <summary>
    /// A menu button that swaps the target image sprite
    /// based on interactable and selection state.
    /// </summary>
    internal class SpriteSwapMenuButton : MenuButton
    {
        [SerializeField, Required]
        [Tooltip("The Image component whose sprite will be updated.")]
        private Image targetImage;

        [Title("Sprites")]
        [SerializeField, Required]
        [Tooltip("Sprite displayed when the button is in its normal state.")]
        private Sprite normalSprite;

        [SerializeField, Required]
        [Tooltip("Sprite displayed when the button is selected.")]
        private Sprite selectedSprite;

        [SerializeField, Required]
        [Tooltip("Sprite displayed when the button is disabled (not interactable).")]
        private Sprite disabledSprite;


        /// <summary>
        /// Updates the target image sprite based on the current
        /// interactable and selection state of the button.
        /// </summary>
        protected override void RefreshVisual()
        {
            // Disabled state
            if (!IsInteractable)
            {
                if (disabledSprite != null)
                {
                    targetImage.sprite = disabledSprite;
                }
                return;
            }

            // Locked selection sprite (only if the button is selected)
            if (LockSelectSprite)
            {
                targetImage.sprite = selectedSprite;
                return;
            }

            // Normal behavior
            targetImage.sprite = IsSelected ? selectedSprite : normalSprite;
        }
    }
}

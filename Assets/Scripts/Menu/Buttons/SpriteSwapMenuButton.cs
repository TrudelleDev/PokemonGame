using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu
{
    /// <summary>
    /// A menu button that swaps the target image sprite
    /// based on interactable and selection state.
    /// </summary>
    public class SpriteSwapMenuButton : MenuButton
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
            if (!IsInteractable)
            {
                if (disabledSprite != null)
                {
                    targetImage.sprite = disabledSprite;
                }

                return;
            }

            targetImage.sprite = IsSelected ? selectedSprite : normalSprite;
        }
    }
}

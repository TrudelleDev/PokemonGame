using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu
{
    /// <summary>
    /// A menu button that displays a cursor arrow sprite
    /// when the button is both interactable and selected.
    /// </summary>
    public class CursorMenuButton : MenuButton
    {
        [SerializeField, Required]
        [Tooltip("The arrow image displayed when this button is selected.")]
        private Image cursorArrow;

        /// <summary>
        /// Updates the cursor arrow visibility based on the current
        /// interactable and selection state of the button.
        /// </summary>
        protected override void RefreshVisual()
        {
            if (cursorArrow == null)
            {
                return;
            }

            // Only show the arrow if interactable AND selected
            cursorArrow.enabled = IsInteractable && IsSelected;
        }
    }
}
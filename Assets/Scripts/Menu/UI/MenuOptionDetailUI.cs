using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Menu.UI
{
    /// <summary>
    /// Displays a displayable object's description and icon.
    /// </summary>
    public class MenuOptionDetailUI : MonoBehaviour, IBindable<IDisplayable>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text field for the description.")]
        private TextMeshProUGUI descriptionText;

        [SerializeField, Required]
        [Tooltip("Image for the icon.")]
        private Image iconImage;

        /// <summary>
        /// Binds to the given displayable object, or clears if null.
        /// </summary>
        /// <param name="displayable">Object providing description and icon.</param>
        public void Bind(IDisplayable displayable)
        {
            if (displayable == null)
            {
                Unbind();
                return;
            }

            descriptionText.text = displayable.Description;
            iconImage.sprite = displayable.Icon;
            iconImage.enabled = iconImage.sprite != null;
        }

        /// <summary>
        /// Clears the current binding and hides the UI.
        /// </summary>
        public void Unbind()
        {
            descriptionText.text = string.Empty;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}

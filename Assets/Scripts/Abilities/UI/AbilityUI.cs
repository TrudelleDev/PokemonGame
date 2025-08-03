using PokemonGame.Abilities.Interfaces;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Abilities.UI
{
    /// <summary>
    /// Displays information about a Pokémon's ability.
    /// </summary>
    [DisallowMultipleComponent]
    public class AbilityUI : MonoBehaviour, IAbilityBindable, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text component for the ability's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text component for the ability's description.")]
        private TextMeshProUGUI descriptionText;

        /// <summary>
        /// Binds the Pokémon's ability information to the UI.
        /// </summary>
        /// <param name="ability">The ability to bind.</param>
        public void Bind(Ability ability)
        {
            if (ability == null || ability.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = ability.Definition.DisplayName;
            descriptionText.text = ability.Definition.Description;
        }

        /// <summary>
        /// Clears the Pokémon's ability information from the UI.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            descriptionText.text = string.Empty;
        }
    }
}

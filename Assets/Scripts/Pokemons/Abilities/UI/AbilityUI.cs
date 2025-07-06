using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities.UI
{
    /// <summary>
    /// Displays a Pokémon's ability name and effect in the UI.
    /// Typically used in summary or status screens.
    /// </summary>
    public class AbilityUI : MonoBehaviour, IAbilityBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text component displaying the ability's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text component displaying the ability's effect description.")]
        private TextMeshProUGUI effectText;

        /// <summary>
        /// Binds the given ability data to the UI elements.
        /// </summary>
        /// <param name="ability">The ability to display.</param>
        public void Bind(Ability ability)
        {
            nameText.text = ability.Data.AbilityName;
            effectText.text = ability.Data.Effect;
        }

        /// <summary>
        /// Clears the ability UI elements.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            effectText.text = string.Empty;
        }
    }
}

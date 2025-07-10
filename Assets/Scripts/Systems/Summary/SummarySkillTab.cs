using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Abilities.UI;
using PokemonGame.Pokemons.UI;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Summary
{
    /// <summary>
    /// Displays detailed Pok�mon stats and ability information in the summary screen.
    /// Supports dynamic data binding and clears the UI when the Pok�mon or its ability data is missing or invalid.
    /// </summary>
    public class SummarySkillTab : MonoBehaviour, IPokemonBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Group containing base stat UI elements (HP, Attack, Defense, etc.).")]
        private PokemonStatsUI statsUI;

        [SerializeField, Required]
        [Tooltip("Group containing the Pok�mon's ability name and effect description.")]
        private AbilityUI abilityUI;

        /// <summary>
        /// Binds the specified Pok�mon data to the stat and ability UI groups.
        /// Clears the UI if the Pok�mon or its ability data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pok�mon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null || pokemon?.Ability?.Data == null)
            {
                Unbind();
                return;
            }

            statsUI.Bind(pokemon);
            abilityUI.Bind(pokemon.Ability);
        }

        /// <summary>
        /// Clears the stat and ability UI elements.
        /// </summary>
        public void Unbind()
        {
            statsUI.Unbind();
            abilityUI.Unbind();
        }
    }
}

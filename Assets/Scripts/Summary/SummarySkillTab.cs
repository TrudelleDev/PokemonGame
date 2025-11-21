using PokemonGame.Ability;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using PokemonGame.Pokemons.UI.Experience;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Displays detailed Pokémon stats and ability information in the summary screen.
    /// Supports dynamic data binding and clears the UI when the Pokémon or its ability data is missing or invalid.
    /// </summary>
    public class SummarySkillTab : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Group containing base stat UI elements (HP, Attack, Defense, etc.).")]
        private PokemonStatsUI statsUI;

        [SerializeField]
        private ExperienceUI experienceUI;


        [SerializeField, Required]
        [Tooltip("Group containing the Pokémon's ability name and effect description.")]
        private AbilityUI abilityUI;

        /// <summary>
        /// Binds the specified Pokémon data to the stat and ability UI groups.
        /// Clears the UI if the Pokémon or its ability data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Definition == null || pokemon?.Ability?.Definition == null)
            {
                Unbind();
                return;
            }

            statsUI.Bind(pokemon);
            experienceUI.Bind(pokemon);
            abilityUI.Bind(pokemon.Ability);
        }

        /// <summary>
        /// Clears the stat and ability UI elements.
        /// </summary>
        public void Unbind()
        {
            statsUI.Unbind();
            experienceUI?.Unbind();
            abilityUI.Unbind();
        }
    }
}

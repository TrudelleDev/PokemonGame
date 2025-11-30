using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Displays detailed Pokémon stats and ability information in the summary screen.
    /// Supports dynamic data binding and clears the UI when the Pokémon data is missing or invalid.
    /// </summary>
    public class SummarySkillTab : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Group containing base stat UI elements (HP, Attack, Defense, etc.).")]
        private PokemonStatsUI statsUI;

        [SerializeField]
        private ExperienceUI experienceUI;

        /// <summary>
        /// Binds the specified Pokémon data to the stat and ability UI groups.
        /// Clears the UI if the Pokémon data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            statsUI.Bind(pokemon);
            experienceUI.Bind(pokemon);
        }

        /// <summary>
        /// Clears the stat and ability UI elements.
        /// </summary>
        public void Unbind()
        {
            statsUI.Unbind();
            experienceUI?.Unbind();
        }
    }
}

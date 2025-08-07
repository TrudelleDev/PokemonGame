using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Summary
{
    /// <summary>
    /// Controls the detailed Pokémon info panel in the summary screen.
    /// Displays Pokédex number, name, unique ID, type icons, and trainer details.
    /// Supports dynamic data binding and clears the UI when data is missing or invalid.
    /// </summary>
    public class SummaryInfoTab : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's overview information such as name, types, and ID.")]
        private PokemonOverviewUI pokemonOverviewUI;

        [SerializeField, Required]
        [Tooltip("Displays the trainer's memo (e.g., caught location, encounter date).")]
        private TrainerMemoUI trainerMemoUI;

        /// <summary>
        /// Binds the specified Pokémon to the overview and trainer memo UI elements.
        /// Clears the UI if the Pokémon or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                Unbind();
                return;
            }

            pokemonOverviewUI.Bind(pokemon);
            trainerMemoUI.Bind(pokemon);
        }

        /// <summary>
        /// Clears the overview and trainer memo UI elements.
        /// </summary>
        public void Unbind()
        {
            pokemonOverviewUI.Unbind();
            trainerMemoUI.Unbind();
        }
    }
}

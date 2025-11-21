using PokemonGame.Party;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Displays detailed information about the selected Pokémon,
    /// including stats, skills, and moves.
    /// </summary>
    public class SummaryView : View
    {
        [SerializeField, Required]
        [Tooltip("Tabs for stats, skills, and move details.")]
        private SummaryTabGroup summaryTabs;

        [SerializeField, Required]
        [Tooltip("Reference to the player's party for selecting the current Pokémon.")]
        private PartyManager party;

        /// <summary>
        /// Called when the view is enabled.
        /// Resets the panel controller and binds the summary tabs to the selected Pokémon.
        /// </summary>
        public void OnEnable()
        {
            PokemonInstance selectedPokemon = party.SelectedPokemon;

            if (selectedPokemon == null)
            {
                summaryTabs.Unbind();
                return;
            }

            summaryTabs.Bind(selectedPokemon);
        }
    }
}

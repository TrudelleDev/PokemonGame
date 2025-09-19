using PokemonGame.Menu.Controllers;
using PokemonGame.Party;
using PokemonGame.Pokemons;
using PokemonGame.Summary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
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
        [Tooltip("Controller for horizontal navigation between summary pages.")]
        private HorizontalPanelController panelController;

        [SerializeField, Required]
        [Tooltip("Reference to the player's party for selecting the current Pokémon.")]
        private PartyManager party;

        /// <summary>
        /// Called when the view is enabled.
        /// Resets the panel controller and binds the summary tabs to the selected Pokémon.
        /// </summary>
        private void OnEnable()
        {
            panelController.ResetController();

            Pokemon selectedPokemon = party.SelectedPokemon;

            if (selectedPokemon == null)
            {
                summaryTabs.Unbind();
                return;
            }

            summaryTabs.Bind(selectedPokemon);
        }
    }
}

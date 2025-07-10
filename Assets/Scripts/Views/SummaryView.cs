using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Systems.Summary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Shows detailed info about the selected Pokémon, including stats, skills, and moves.
    /// </summary>
    public class SummaryView : View
    {
        [SerializeField, Required]
        [Tooltip("Tabs for stats, skills, and move details.")]
        private SummaryTabGroup summaryTabs;

        [SerializeField, Required]
        [Tooltip("Controller for horizontal summary navigation.")]
        private HorizontalPanelController controller;

        [SerializeField, Required]
        [Tooltip("The player's party for selecting the current Pokémon.")]
        private Party party;

        /// <summary>
        /// Called once before the view is shown.
        /// </summary>
        public override void Initialize()
        {
            // Optional one-time setup.
        }

        /// <summary>
        /// Updates the UI when the view becomes active.
        /// </summary>
        private void OnEnable()
        {
            controller.ResetController();

            Pokemon selected = party.SelectedPokemon;

            if (selected == null)
            {
                summaryTabs.Unbind();
                return;
            }

            summaryTabs.Bind(selected);
        }
    }
}

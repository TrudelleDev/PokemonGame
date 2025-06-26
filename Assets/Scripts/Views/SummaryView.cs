using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Moves.UI.Summary;
using PokemonGame.Pokemons.UI.Summary;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Displays detailed information about the currently selected Pokémon,
    /// including stats, moves, and visual info. Updates UI components on enable
    /// and resets navigation controls on disable.
    /// </summary>
    public class SummaryView : View
    {
        [SerializeField] private SummaryHeader header;
        [SerializeField] private SummaryInfoPanel infoPanel;
        [SerializeField] private SummarySkillPanel skillPanel;
        [SerializeField] private SummaryMovePanel movePanel;
        [Space]
        [SerializeField] private Party party;

        private CloseView closeView;
        private HorizontalPanelController summaryViewController;

        public override void Initialize() { }

        private void Awake()
        {
            closeView = GetComponent<CloseView>();
            summaryViewController = GetComponentInChildren<HorizontalPanelController>();
        }

        private void OnEnable()
        {
            if (party.SelectedPokemon == null)
                return;

            Pokemon pokemon = party.SelectedPokemon;

            infoPanel.Bind(pokemon);
            skillPanel.Bind(pokemon);
            header.Bind(pokemon);
            movePanel.Bind(pokemon);
        }

        private void OnDisable()
        {
            summaryViewController.ResetController();
        }

        private void Update()
        {
            // Prevent closing the summary view if move info panel is being shown
           // closeView.enabled = movePanel.IsMoveDescriptionOpen;
        }
    }
}

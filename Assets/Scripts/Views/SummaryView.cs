using PokemonGame.MenuControllers;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Moves.UI.Summary;
using PokemonGame.Pokemons.UI.Summary;
using UnityEngine;

namespace PokemonGame.Views
{
    public class SummaryView : View
    {
        [Header("Summary Sections")]
        [SerializeField] private SummaryPokemonInfo pokemonInfo;
        [SerializeField] private SummaryPokemonSkill pokemonSkill;
        [SerializeField] private SummaryPokemonDisplay pokemonDisplay;
        [SerializeField] private SummaryMovePanel movePanel;
        [Space]
        [SerializeField] private Party party;

        private CloseView closeView;
        private HorizontalPanelController summaryViewController;

        public override void Initialize()
        {
            // Make sure the summary view is subscribe to the pokemon selection event before its open once.
            party.OnSelectPokemon += OnPartySelectPokemon;
           
        }

        private void Awake()
        {
            closeView = GetComponent<CloseView>();
            summaryViewController = GetComponentInChildren<HorizontalPanelController>();
        }

        private void OnDisable()
        {
            // Go back to the first section of the summary view
            summaryViewController.ResetController();
        }

        private void Update()
        {
            // Cant close the summary view if the move panel is open
            closeView.enabled = movePanel.IsMoveDescriptionOpen;
        }

        private void OnPartySelectPokemon(Pokemon pokemon)
        {
            // Bind the selected pokemon data to each section of the summary view
            pokemonInfo.Bind(pokemon);
            pokemonSkill.Bind(pokemon);
            pokemonDisplay.Bind(pokemon);
            movePanel.Bind(pokemon);
        }
    }
}

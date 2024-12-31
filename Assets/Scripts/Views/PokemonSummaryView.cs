using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Moves.UI.Summary;
using PokemonGame.Pokemons.UI.Summary;
using UnityEngine;

namespace PokemonGame.Views
{
    public class PokemonSummaryView : View
    {
        [SerializeField] private SummaryMovePanel movePanel;

        private CloseView closeView;
        private HorizontalMenuController horizontalMenuController;

        public override void Initialize() { }

        private void Awake()
        {
            closeView = GetComponent<CloseView>();
            horizontalMenuController = GetComponent<HorizontalMenuController>();
        }

        private void OnDisable()
        {
            horizontalMenuController.ResetController();
        }

        private void Update()
        {
            // Cant close the summary view if the move panel is open
            closeView.enabled = movePanel.IsMoveDescriptionOpen;
        }
    }
}

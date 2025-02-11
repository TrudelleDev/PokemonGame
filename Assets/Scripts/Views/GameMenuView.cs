using PokemonGame.GameState;
using UnityEngine;

namespace PokemonGame.Views
{
    public class GameMenuView : View
    {
        [SerializeField] private MenuButton pokedex;
        [SerializeField] private MenuButton party;
        [SerializeField] private MenuButton inventory;
        [SerializeField] private MenuButton trainerCard;
        [SerializeField] private MenuButton save;
        [SerializeField] private MenuButton option;
        [SerializeField] private MenuButton exit;

        public override void Initialize()
        {
            pokedex.OnClick += () => ViewManager.Instance.Show<PokedexView>();
            party.OnClick += () => ViewManager.Instance.Show<PartyMenuView>();
            inventory.OnClick += () => ViewManager.Instance.Show<BagView>();
            trainerCard.OnClick += () => ViewManager.Instance.Show<TrainerCardView>();
            exit.OnClick += () => OnExitClick();
        }

        private void OnExitClick()
        {
            GameStateManager.Instance.SetState(GameState.GameState.Resume);
            ViewManager.Instance.ShowLast();
        }
    }
}

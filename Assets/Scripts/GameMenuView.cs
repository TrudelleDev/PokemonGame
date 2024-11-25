using PokemonGame.Characters.PokemonTeam.UI;
using PokemonGame.Encyclopedia.UI;
using UnityEngine;

namespace PokemonGame
{
    public class GameMenuView : View
    {
        [SerializeField] private SelectableUIElement pokedex;
        [SerializeField] private SelectableUIElement party;
        [SerializeField] private SelectableUIElement inventory;
        [SerializeField] private SelectableUIElement trainerCard;
        [SerializeField] private SelectableUIElement save;
        [SerializeField] private SelectableUIElement option;
        [SerializeField] private SelectableUIElement exit;

        public void Awake()
        {
            pokedex.OnClick += () => ViewManager.Instance.Show<PokedexView>();
            party.OnClick += () => ViewManager.Instance.Show<PartyMenuView>();
            exit.OnClick += () => ViewManager.Instance.ShowLast();
        }

        public override void Initialize()
        {
            MenuToggler.OnOpenMenu += OnGameMenuTogglerOpenMenu;
        }

        private void OnGameMenuTogglerOpenMenu()
        {
            if (!gameObject.activeInHierarchy && ViewManager.Instance.IsHistoryEmpty())
            {
                ViewManager.Instance.Show<GameMenuView>();
            }
            else
            {
                ViewManager.Instance.ShowLast();
            }
        }
    }

}

using UnityEngine;

namespace PokemonGame.Views
{
    public class GameMenuView : View
    {
        [SerializeField] private MenuButton party;
        [SerializeField] private MenuButton inventory;
        [SerializeField] private MenuButton exit;

        public override void Initialize()
        {
            party.OnClick += () => ViewManager.Instance.Show<PartyMenuView>();
            inventory.OnClick += () => ViewManager.Instance.Show<InventoryView>();
            exit.OnClick += () => ViewManager.Instance.GoToPreviousView();
        }
    }
}

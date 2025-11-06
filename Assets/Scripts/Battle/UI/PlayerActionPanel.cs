using PokemonGame.Battle.States;
using PokemonGame.Dialogue;
using PokemonGame.Inventory;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Party;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// Handles the player's available battle actions: Fight, Bag, Pokémon, and Run.
    /// Acts as a local UI panel within the <see cref="BattleView"/>, managing transitions
    /// to other views (move selection, inventory, party, etc.).
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerActionPanel : View
    {
        [SerializeField, Required]
        [Tooltip("Button that opens the Move Selection Panel.")]
        private MenuButton fightButton;

        [SerializeField, Required]
        [Tooltip("Button that opens the player's Inventory view.")]
        private MenuButton bagButton;

        [SerializeField, Required]
        [Tooltip("Button that opens the Party Menu view.")]
        private MenuButton partyButton;

        [SerializeField, Required]
        [Tooltip("Button that triggers a run attempt from battle.")]
        private MenuButton runButton;

        private BattleView battle;

        private void Awake()
        {
            // Register click event handlers
            fightButton.OnClick += ShowMovePanel;
            bagButton.OnClick += ShowInventory;
            partyButton.OnClick += ShowParty;
            runButton.OnClick += RunAway;
        }

        private void Start()
        {
            // Cache the BattleView reference
            battle = ViewManager.Instance.Get<BattleView>();
        }

        private void OnDestroy()
        {
            // Unregister event handlers to prevent memory leaks
            fightButton.OnClick -= ShowMovePanel;
            bagButton.OnClick -= ShowInventory;
            partyButton.OnClick -= ShowParty;
            runButton.OnClick -= RunAway;
        }

        private void ShowMovePanel()
        {
            if (battle == null)
                return;

            ViewManager.Instance.CloseTopView();
            ViewManager.Instance.Show<MoveSelectionView>();
        }

        private void ShowInventory()
        {
            ViewManager.Instance.Show<InventoryView>();
        }

        private void ShowParty()
        {
            ViewManager.Instance.Show<PartyMenuView>();
        }

        private void RunAway()
        {
            ViewManager.Instance.CloseTopView();
            battle.StateMachine.SetState(new BattleRunState(battle.StateMachine));
        }

        /// <summary>
        /// Disables input control for this panel (used during transitions).
        /// </summary>
        public override void Freeze()
        {
            GetComponent<GridMenuController>().enabled = false;
        }

        /// <summary>
        /// Re-enables input control for this panel.
        /// </summary>
        public override void Unfreeze()
        {
            GetComponent<GridMenuController>().enabled = true;
        }
    }
}

using System.Collections;
using PokemonGame.Inventory;
using PokemonGame.Inventory.UI;
using PokemonGame.Inventory.UI.InventoryOptions;
using PokemonGame.Party;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles opening the inventory during battle and reacting to item usage or cancellation.
    /// Continues the battle flow after item usage dialogue finishes.
    /// </summary>
    public sealed class UseItemState : IBattleState
    {
        private readonly BattleStateMachine machine;

        private InventoryView inventoryView;
        private InventoryPresenter inventoryPresenter;
        private InventoryOptionsPresenter optionsPresenter;

        public UseItemState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            // Show inventory UI
            inventoryView = ViewManager.Instance.Show<InventoryView>();
            inventoryPresenter = inventoryView.GetComponent<InventoryPresenter>();

            // Subscribe to cancellation
            inventoryView.CancelRequested += OnCancel;
            inventoryPresenter.ItemUsed += OnItemUsed;


        }

        public void Update() { }

        public void Exit()
        {
            inventoryPresenter.ItemUsed -= OnItemUsed;
            inventoryPresenter = null;
            inventoryView.CancelRequested -= OnCancel;
            inventoryView = null;

            ViewManager.Instance.CloseAll(typeof(InventoryView));
        }

        private void OnCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }

        private void OnItemUsed(bool used)
        {
            if (used)
            {           
               machine.SetState(new OpponentTurnState(machine));
            }
        }
    }
}

using System;
using PokemonGame.Inventory.UI;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles opening the inventory during battle and transitioning based on item usage or cancellation.
    /// </summary>
    internal sealed class UseItemState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private InventoryView inventoryView;
        private InventoryPresenter inventoryPresenter;

        internal UseItemState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            inventoryView = ViewManager.Instance.Show<InventoryView>();
            inventoryPresenter = inventoryView.GetComponent<InventoryPresenter>();

            inventoryView.CancelRequested += HandleCancel;
            inventoryPresenter.ItemUsed += HandleItemUsed;
        }

        public void Update() { }

        public void Exit()
        {
            if (inventoryPresenter != null)
            {
                inventoryPresenter.ItemUsed -= HandleItemUsed;
                inventoryPresenter = null;
            }

            if (inventoryView != null)
            {
                inventoryView.CancelRequested -= HandleCancel;
                inventoryView = null;
            }

            ViewManager.Instance.CloseAll(typeof(InventoryView));
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        } 

        private void HandleItemUsed(bool used)
        {
            if (used)
            {
                machine.SetState(new OpponentTurnState(machine));
            }
        }
    }
}
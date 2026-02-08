using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;
using MonsterTamer.Inventory.UI;
using MonsterTamer.Views;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles opening the player's inventory during battle,
    /// and transitions the battle state based on item usage or cancellation.
    /// </summary>
    internal sealed class PlayerInventoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private InventoryView inventoryView;
        private InventoryPresenter inventoryPresenter;

        /// <summary>
        /// Creates a new player inventory state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerInventoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state, shows the inventory UI, and subscribes to events.
        /// </summary>
        public void Enter()
        {
            inventoryView = ViewManager.Instance.Show<InventoryView>();
            inventoryPresenter = inventoryView.GetComponent<InventoryPresenter>();

            inventoryView.ReturnRequested += HandleCancel;
            inventoryPresenter.ItemUsed += HandleItemUsed;
        }

        /// <summary>
        /// No per-frame logic is required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Exits the state and unsubscribes from all inventory events.
        /// </summary>
        public void Exit()
        {
            if (inventoryPresenter != null)
            {
                inventoryPresenter.ItemUsed -= HandleItemUsed;
                inventoryPresenter = null;
            }

            if (inventoryView != null)
            {
                inventoryView.ReturnRequested -= HandleCancel;
                inventoryView = null;
            }
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionMenuState(machine));
        }

        private void HandleItemUsed(bool used)
        {
            if (used)
            {
                // Ensure the inventory UI is closed before proceeding
                ViewManager.Instance.ForceClose(typeof(InventoryView));
                machine.SetState(new OpponentTurnState(machine));
            }
        }
    }
}

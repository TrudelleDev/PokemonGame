using System.Collections;
using PokemonGame.Inventory;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the sequence of opening the inventory, allowing the player to select and use an item.
    /// Transitions to the opponent's turn if an item is successfully used, or back to 
    /// the action state on cancel.
    /// </summary>
    public sealed class UseItemState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private InventoryView inventoryView;
        private InventoryItemOptionsView optionsView;

        private BattleView BattleView => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="UseItemState"/>.
        /// </summary>
        /// <param name="machine">The <see cref="BattleStateMachine"/> controlling the battle flow.</param>
        public UseItemState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        // --- IBattleState Implementation ---

        /// <summary>
        /// Called when entering this state. Shows the inventory UI and sets up event listeners.
        /// </summary>
        public void Enter()
        {
            // Show the main inventory view
            inventoryView = ViewManager.Instance.Show<InventoryView>();

            // Retrieve the options view (assuming it is part of or accessible from the inventory setup)
            optionsView = ViewManager.Instance.Get<InventoryItemOptionsView>();

            if(inventoryView != null)
            {
                inventoryView.OnCloseKeyPress += OnCancel;

                if (inventoryView.CancelButton != null)
                {
                    inventoryView.CancelButton.OnClick += OnCancel;
                }
            }
                
            // Subscribe to inputs
            if (inventoryView.CancelButton != null)
            {
                inventoryView.CancelButton.OnClick += OnCancel;
            }

            if (optionsView != null)
            {
                optionsView.OnItemUsed += OnItemUsed;
            }
        }

        public void Update() { }

        /// <summary>
        /// Cleans up when exiting the state. Unsubscribes event listeners and closes inventory UI.
        /// </summary>
        public void Exit()
        {
            // Unsubscribe events safely
            if (inventoryView != null)
            {
                inventoryView.OnCloseKeyPress -= OnCancel;

                if (inventoryView.CancelButton != null)
                {
                    inventoryView.CancelButton.OnClick -= OnCancel;
                }
            }

            if (optionsView != null)
            {
                optionsView.OnItemUsed -= OnItemUsed;
            }

            // Close views
            ViewManager.Instance.Close<InventoryView>();
            ViewManager.Instance.Close<InventoryItemOptionsView>();

            // Clear local references
            inventoryView = null;
            optionsView = null;
        }

        // --- Event Handlers (Transitions) ---

        private void OnItemUsed(bool used)
        {
            // If the item was successfully used (e.g., healing was possible, status effect applied)
            if (used)
            {
                // Item usage counts as the player's turn, so transition to opponent's turn.
                BattleView.StartCoroutine(WaitForTransitionThenOpponentTurn());
            }
        }

        private IEnumerator WaitForTransitionThenOpponentTurn()
        {
            // Ensure any visual transitions from item usage are complete
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);
            machine.SetState(new OpponentTurnState(machine));
        }

        private void OnCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }
    }
}
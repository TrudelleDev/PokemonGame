using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.UI;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Displays the primary action menu (Fight, Bag, Monsters, Flee)
    /// and handles player action selection.
    /// </summary>
    internal sealed class PlayerActionMenuState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleActionView actionPanel;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player action menu state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerActionMenuState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins UI setup once transitions complete.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(SetupUIAndAwaitInput());
        }

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Cleans up UI bindings and closes the action menu.
        /// </summary>
        public void Exit()
        {
            if (actionPanel == null)
            {
                return;
            }

            actionPanel.MoveSelectionRequested -= HandleMoveSelectionRequested;
            actionPanel.PartyRequested -= HandlePartyRequested;
            actionPanel.InventoryRequested -= HandleInventoryRequested;
            actionPanel.EscapeRequested -= HandleEscapeRequested;

            ViewManager.Instance.Close<BattleActionView>();
            Battle.DialogueBox.Clear();

            actionPanel = null;
        }

        /// <summary>
        /// Waits for view transitions to finish, then displays the action menu
        /// and binds input events.
        /// </summary>
        private IEnumerator SetupUIAndAwaitInput()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            actionPanel = ViewManager.Instance.Show<BattleActionView>();

            string message = string.Format(
                BattleMessages.ChooseAction,
                Battle.PlayerActiveMonster.Definition.DisplayName);

            Battle.DialogueBox.ShowPrompt(message);

            actionPanel.MoveSelectionRequested += HandleMoveSelectionRequested;
            actionPanel.PartyRequested += HandlePartyRequested;
            actionPanel.InventoryRequested += HandleInventoryRequested;
            actionPanel.EscapeRequested += HandleEscapeRequested;
        }

        private void HandleMoveSelectionRequested()
        {
            machine.SetState(new PlayerMoveSelectState(machine));
        }

        private void HandlePartyRequested()
        {
            machine.SetState(new PlayerPartySelectState(machine));
        }

        private void HandleInventoryRequested()
        {
            machine.SetState(new PlayerInventoryState(machine));
        }

        private IEnumerator ShowEscapeFailDialogue()
        {
            ViewManager.Instance.Close<BattleActionView>();
            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(BattleMessages.EscapeTrainer);
            machine.SetState(new PlayerActionMenuState(machine));
        }

        private void HandleEscapeRequested()
        {
            // Only escape wild battle
            if (Battle.Opponent == null)
            {
                machine.SetState(new PlayerEscapeState(machine));
            }
            else
            {
                Battle.StartCoroutine(ShowEscapeFailDialogue());
            }
        }
    }
}

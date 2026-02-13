using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.UI;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Displays the primary action menu (Fight, Bag, Monsters, Flee) and handles player selection.
    /// </summary>
    internal sealed class PlayerActionMenuState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleActionView actionPanel;

        private BattleView Battle => machine.BattleView;

        internal PlayerActionMenuState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(SetupUIAndAwaitInput());
        public void Update() { }

        public void Exit()
        {
            if (actionPanel == null) return;

            UnbindEvents();
            ViewManager.Instance.Close<BattleActionView>();
            actionPanel = null;
        }

        private IEnumerator SetupUIAndAwaitInput()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            actionPanel = ViewManager.Instance.Show<BattleActionView>();

            var chooseActionMessage = BattleMessages.ChooseAction(Battle.PlayerActiveMonster.Definition.DisplayName);
            Battle.DialogueBox.ShowPrompt(chooseActionMessage);

            BindEvents();
        }

        private void BindEvents()
        {
            actionPanel.MoveSelectionRequested += HandleMoveSelectionRequested;
            actionPanel.PartyRequested += HandlePartyRequested;
            actionPanel.InventoryRequested += HandleInventoryRequested;
            actionPanel.EscapeRequested += HandleEscapeRequested;
        }

        private void UnbindEvents()
        {
            actionPanel.MoveSelectionRequested -= HandleMoveSelectionRequested;
            actionPanel.PartyRequested -= HandlePartyRequested;
            actionPanel.InventoryRequested -= HandleInventoryRequested;
            actionPanel.EscapeRequested -= HandleEscapeRequested;
        }

        private void HandleEscapeRequested()
        {
            if (!Battle.Opponent)      
                machine.SetState(new PlayerEscapeState(machine));     
            else     
                Battle.StartCoroutine(ShowEscapeFailDialogue());      
        }

        private IEnumerator ShowEscapeFailDialogue()
        {
            ViewManager.Instance.Close<BattleActionView>();

            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(BattleMessages.EscapeTrainer);
            machine.SetState(new PlayerActionMenuState(machine));
        }

        private void HandleMoveSelectionRequested() => machine.SetState(new PlayerMoveSelectState(machine));
        private void HandlePartyRequested() => machine.SetState(new PlayerPartySelectState(machine));
        private void HandleInventoryRequested() => machine.SetState(new PlayerInventoryState(machine));
    }
}

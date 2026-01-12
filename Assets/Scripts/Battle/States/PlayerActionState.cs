using System.Collections;
using PokemonGame.Battle.UI;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Displays the primary action menu (Fight, Bag, Pokémon, Run) and manages player selection.
    /// </summary>
    internal sealed class PlayerActionState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleActionView actionPanel;

        private BattleView Battle => machine.BattleView;

        internal PlayerActionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            Battle.StartCoroutine(SetupUIAndAwaitInput());
        }

        public void Update() { }

        public void Exit()
        {
            if (actionPanel == null)
                return;

            actionPanel.FightSelected -= HandleFightSelected;
            actionPanel.PartySelected -= HandlePartySelected;
            actionPanel.BagSelected -= HandleBagSelected;
            actionPanel.RunSelected -= HandleRunSelected;

            ViewManager.Instance.Close<BattleActionView>();
            Battle.DialogueBox.Clear();

            actionPanel = null;
        }

        private IEnumerator SetupUIAndAwaitInput()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            actionPanel = ViewManager.Instance.Show<BattleActionView>();

            Battle.DialogueBox.ShowPrompt($"What will\n{Battle.PlayerActivePokemon.Definition.DisplayName} do?");

            actionPanel.FightSelected += HandleFightSelected;
            actionPanel.PartySelected += HandlePartySelected;
            actionPanel.BagSelected += HandleBagSelected;
            actionPanel.RunSelected += HandleRunSelected;
        }

        private void HandleFightSelected() => machine.SetState(new MoveSelectionState(machine));
        private void HandlePartySelected() => machine.SetState(new SelectPokemonState(machine));
        private void HandleBagSelected() => machine.SetState(new UseItemState(machine));
        private void HandleRunSelected() => machine.SetState(new EscapeState(machine));
    }
}
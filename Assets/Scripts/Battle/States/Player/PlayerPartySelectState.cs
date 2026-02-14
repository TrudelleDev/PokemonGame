using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Dialogue;
using MonsterTamer.Party.Enums;
using MonsterTamer.Party.UI;
using MonsterTamer.Party.UI.PartyOptions;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles party selection. Transitions to SendOut if forced (fainted) or Swap if voluntary.
    /// </summary>
    internal sealed class PlayerPartySelectState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly bool isForced;

        private PartyMenuView partyView;
        private PartyMenuPresenter partyPresenter;
        private PartyMenuOptionsView optionsView;

        private BattleView Battle => machine.BattleView;

        // Modern single-line constructor
        internal PlayerPartySelectState(BattleStateMachine machine, bool isForced = false)
            => (this.machine, this.isForced) = (machine, isForced);

        public void Enter()
        {
            partyView = ViewManager.Instance.Show<PartyMenuView>();
            partyPresenter = partyView.GetComponent<PartyMenuPresenter>();
            optionsView = ViewManager.Instance.Get<PartyMenuOptionsView>();

            partyPresenter.Setup(PartySelectionMode.Battle);

            if (!isForced) partyPresenter.ReturnRequested += HandleCancel;
            optionsView.SwapRequested += HandleSwapRequested;

            partyView.RefreshSlots();
        }

        public void Exit()
        {
            if (partyPresenter != null)
            {
                partyPresenter.ReturnRequested -= HandleCancel;
            }
         
            if (optionsView != null)
            {
                optionsView.SwapRequested -= HandleSwapRequested;
            }          

            ViewManager.Instance.Close<PartyMenuOptionsView>();
            ViewManager.Instance.Close<PartyMenuView>();
        }

        public void Update() { }

        private IEnumerator PlayTransitionSequence()
        {
            var dialogue = OverworldDialogueBox.Instance.Dialogue;
            var monster = Battle.Player.Party.SelectedMonster;

            // 1. Validation: Must have HP
            if (monster.IsFainted)
            {
                ViewManager.Instance.Close<PartyMenuOptionsView>();
                var noEnergyMessage = BattleMessages.MonsterHasNoEnergy(monster.Definition.DisplayName);

                yield return dialogue.ShowDialogueAndWaitForInput(noEnergyMessage);
                partyView.RefreshSlots();
                yield break;
            }

            // 2. Validation: Cannot swap to self
            if (monster == Battle.PlayerActiveMonster)
            {
                ViewManager.Instance.Close<PartyMenuOptionsView>();
                yield return dialogue.ShowDialogueAndWaitForInput(BattleMessages.MonsterAlreadyInBattle);
                partyView.RefreshSlots();
                yield break;
            }

            // 3. Success: Clean up and Transition
            Exit();
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            if (isForced)
            {
                machine.SetState(new PlayerForcedSendOutState(machine, monster));
            }
            else
            {
                // Voluntary swap consumes the turn; AI selects a move
                var opponentMove = Battle.OpponentActiveMonster.GetRandomMove();
                machine.SetState(new PlayerSwapMonsterState(machine, monster, opponentMove));
            }
        }

        private void HandleSwapRequested() => Battle.StartCoroutine(PlayTransitionSequence());
        private void HandleCancel() => machine.SetState(new PlayerActionMenuState(machine));
    }
}

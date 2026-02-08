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
    /// Handles party selection during battle, including forced and optional swaps.
    /// </summary>
    internal sealed class PlayerPartySelectState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly bool isForced;

        private PartyMenuView partyView;
        private PartyMenuPresenter partyPresenter;
        private PartyMenuOptionsView optionsView;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new party selection state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        /// <param name="isForced">
        /// Whether the player is required to select a new monster
        /// (e.g. after fainting).
        /// </param>
        internal PlayerPartySelectState(BattleStateMachine machine, bool isForced = false)
        {
            this.machine = machine;
            this.isForced = isForced;
        }

        /// <summary>
        /// Enters the state and displays the party menu.
        /// </summary>
        public void Enter()
        {
            partyView = ViewManager.Instance.Show<PartyMenuView>();
            partyPresenter = partyView.GetComponent<PartyMenuPresenter>();
            optionsView = ViewManager.Instance.Get<PartyMenuOptionsView>();

            partyPresenter.Setup(PartySelectionMode.Battle);

            if (!isForced)
            {
                partyPresenter.ReturnRequested += HandleCancel;
            }

            optionsView.SwapRequested += HandleSwapRequested;
            partyView.RefreshSlots();
        }

        /// <summary>
        /// Exits the state and cleans up UI bindings.
        /// </summary>
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

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Validates the selected monster and transitions to the appropriate
        /// battle state if the selection is valid.
        /// </summary>
        private IEnumerator PlayTransitionSequence()
        {
            var dialogue = OverworldDialogueBox.Instance.Dialogue;
            var monster = Battle.Player.Party.SelectedMonster;

            // Validation: selected monster must be usable
            if (monster.Health.CurrentHealth <= 0)
            {
                ViewManager.Instance.Close<PartyMenuOptionsView>();

                string message = string.Format(
                    BattleMessages.MonsterHasNoEnergy,
                    monster.Definition.DisplayName);

                yield return dialogue.ShowDialogueAndWaitForInput(message);

                partyView.RefreshSlots();
                yield break;
            }

            // Validation: selected monster cannot already be in battle
            if (monster == Battle.PlayerActiveMonster)
            {
                ViewManager.Instance.Close<PartyMenuOptionsView>();
                yield return dialogue.ShowDialogueAndWaitForInput(BattleMessages.MonsterAlreadyInBattle);

                partyView.RefreshSlots();
                yield break;
            }

            // Successful selection
            Exit();
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            if (isForced)
            {
                machine.SetState(new PlayerForcedSendOutState(machine, monster));
            }
            else
            {
                machine.SetState(new PlayerSwapMonsterState(machine, monster));
            }
        }

        private void HandleSwapRequested()
        {
            Battle.StartCoroutine(PlayTransitionSequence());
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionMenuState(machine));
        }
    }
}

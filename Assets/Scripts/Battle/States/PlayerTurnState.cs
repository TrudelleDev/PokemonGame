using System;
using System.Collections;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Executes the player's selected move and transitions to either OpponentTurnState or OpponentFaintedState.
    /// </summary>
    internal sealed class PlayerTurnState : IBattleState
    {
        private const float TurnDelay = 0.5f;

        private readonly BattleStateMachine machine;
        private readonly MoveInstance move;

        private BattleView Battle => machine.BattleView;

        internal PlayerTurnState(BattleStateMachine machine, MoveInstance move)
        {
            this.machine = machine;
            this.move = move;
        }

        public void Enter()
        {
            Battle.Components.Animation.PlayPlayerHUDDefault();
            Battle.Components.Animation.PlayPlayerPokemonDefault();

            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }

        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.PlayerActivePokemon;
            var target = Battle.OpponentActivePokemon;
            var context = new MoveContext(Battle, user, target, move);

            // 1. Announce Move
            yield return Battle.DialogueBox.ShowDialogueAndWait(
                $"{user.Definition.DisplayName} used {move.Definition.DisplayName}!"
            );

            // 2. Execute Sequence
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            // 3. Check for Faint
            if (target.Health.CurrentHealth <= 0)
            {
                machine.SetState(new OpponentFaintedState(machine, target));
                yield break;
            }

            // 4. Transition to Opponent
            yield return new WaitForSecondsRealtime(TurnDelay);
            machine.SetState(new OpponentTurnState(machine));
        }
    }
}
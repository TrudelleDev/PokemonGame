using System.Collections;
using PokemonGame.Move.Models;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the opponent's turn, including move selection, execution, and faint checks.
    /// </summary>
    internal sealed class OpponentTurnState : IBattleState
    {
        private const float TurnPause = 0.5f;
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal OpponentTurnState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            // Reset visuals to default state for the new turn
            Battle.Components.Animation.PlayPlayerHUDDefault();
            Battle.Components.Animation.PlayPlayerPokemonDefault();

            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }

        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            // Wait for any UI transitions to finish before starting
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var user = Battle.OpponentActivePokemon;
            var target = Battle.PlayerActivePokemon;

            // Simple AI: Select the first available move
            var move = user.Moves.Moves[0];
            var context = new MoveContext(Battle, user, target, move);

            // 1. Announcement
            yield return Battle.DialogueBox.ShowDialogueAndWait($"{user.Definition.DisplayName} used {move.Definition.DisplayName}!");

            // 2. Execution (Animations, Damage, Effects)
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            // 3. Faint Check
            if (target.Health.CurrentHealth <= 0)
            {
                machine.SetState(new PlayerFaintedState(machine, target));
                yield break;
            }

            // 4. Return control to Player
            yield return new WaitForSecondsRealtime(TurnPause);
            machine.SetState(new PlayerActionState(machine));
        }
    }
}
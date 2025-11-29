using System.Collections;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the player's attack turn.
    /// Executes the move, waits for animations,
    /// and transitions to the next state.
    /// </summary>
    public sealed class PlayerTurnState : IBattleState
    {
        private const float TurnDelay = 0.5f;
        private readonly BattleStateMachine machine;
        private readonly MoveInstance move;

        private BattleView Battle => machine.BattleView;

        public PlayerTurnState(BattleStateMachine machine, MoveInstance move)
        {
            this.machine = machine;
            this.move = move;
        }

        public void Enter()
        {
            Battle.StartCoroutine(ExecuteTurn());
        }

        private IEnumerator ExecuteTurn()
        {
            PokemonInstance user = Battle.PlayerPokemon;
            PokemonInstance target = Battle.OpponentPokemon;
            MoveContext context = new MoveContext(Battle, user, target, move);

            yield return Battle.DialogueBox.ShowDialogueAndWait($"{user.Definition.DisplayName} used {move.Definition.DisplayName}!");
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);
  
            // 6. Check for opponent fainted state
            if (target.Health.CurrentHealth <= 0)
            {
                yield return Battle.BattleAnimation.PlayOpponentDeath();
                yield return Battle.DialogueBox.ShowDialogueAndWait($"Wild {target.Definition.DisplayName} fainted!", manualArrowControl: true);
                yield return Battle.DialogueBox.WaitForPlayerAdvance();

                machine.SetState(new VictoryState(machine));
                yield break; // End the coroutine
            }

            yield return new WaitForSecondsRealtime(TurnDelay);
            machine.SetState(new OpponentTurnState(machine));
        }

        // IBattleState members required but not used in this state's flow
        public void Update() { }
        public void Exit() { }
    }
}
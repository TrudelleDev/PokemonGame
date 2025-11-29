using System.Collections;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    public class OpponentTurnState : IBattleState
    {
        private const float TurnPause = 0.5f;
        private readonly BattleStateMachine machine;

        private BattleView Battle => machine.BattleView;

        public OpponentTurnState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            Battle.StartCoroutine(ExecuteTurn());
        }

        private IEnumerator ExecuteTurn()
        {
            PokemonInstance user = Battle.OpponentPokemon;
            PokemonInstance target = Battle.PlayerPokemon;
            MoveInstance move = user.Moves.Moves[0];
            MoveContext context = new MoveContext(Battle, user, target, move);

            yield return Battle.DialogueBox.ShowDialogueAndWait($"{user.Definition.DisplayName} used {move.Definition.DisplayName}!");
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            if (target.Health.CurrentHealth <= 0)
            {
                yield return Battle.DialogueBox.ShowDialogueAndWait($"{target.Definition.DisplayName} fainted!");
                yield return Battle.BattleAnimation.PlayPlayerDeath();
                yield break;
            }

            yield return new WaitForSecondsRealtime(TurnPause);
            machine.SetState(new PlayerActionState(machine));
        }

        public void Update() { }
        public void Exit() { }
    }
}

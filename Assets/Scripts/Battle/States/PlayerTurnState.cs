using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Executes the player's selected move and transitions the flow to the opponent's turn 
    /// or a terminal state (e.g., VictoryState) if the opponent faints.
    /// </summary>
    public sealed class PlayerTurnState : IBattleState
    {
        private const float TurnDelay = 0.5f;

        private readonly BattleStateMachine machine;
        private readonly MoveInstance move;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player turn state for the selected move.
        /// </summary>
        /// <param name="machine">The battle state machine controlling the current battle flow.</param>
        /// <param name="move">The move instance selected by the player to execute.</param>
        public PlayerTurnState(BattleStateMachine machine, MoveInstance move)
        {
            this.machine = machine;
            this.move = move;
        }

        /// <summary>
        /// Called when entering the state. Initializes animations and starts the move execution coroutine.
        /// </summary>
        public void Enter()
        {
            // Reset HUD and Pokemon visuals to default turn state
            Battle.Components.Animation.PlayPlayerHUDDefault();
            Battle.Components.Animation.PlayPlayerPokemonDefault();

            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }

        /// <summary>
        /// No cleanup required when leaving this state.
        /// </summary>
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            // Wait until scene transitions are complete
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            var user = Battle.PlayerPokemon;
            var target = Battle.OpponentPokemon;

            // Create context for move execution
            var context = new MoveContext(Battle, user, target, move);

            // 1. Announce the move
            yield return dialogue.ShowDialogueAndWait(
                $"{user.Definition.DisplayName} used {move.Definition.DisplayName}!"
            );

            // 2. Execute the move sequence (effects, damage, animation)
            yield return move.Definition.MoveEffect.PerformMoveSequence(context);

            // 3. Check for Faint
            if (target.Health.CurrentHealth <= 0)
            {
                yield return PlayFaintSequence(animation, dialogue, target);

                // Transition to victory state
                machine.SetState(new VictoryState(machine));
                yield break;
            }

            // 4. End Turn and Transition
            yield return new WaitForSecondsRealtime(TurnDelay);

            // Transition to opponent's turn
            machine.SetState(new OpponentTurnState(machine));
        }

        private IEnumerator PlayFaintSequence(
            BattleAnimation animation,
            DialogueBox dialogue,
            PokemonInstance opponent)
        {
            yield return animation.PlayOpponentDeath();
            yield return animation.PlayOpponentHudExit();

            yield return dialogue.ShowDialogueAndWaitForPlayerAdvance(
                 $"Wild {opponent.Definition.DisplayName}\nfainted!",
                manualArrowControl: true
            );
        }
    }
}
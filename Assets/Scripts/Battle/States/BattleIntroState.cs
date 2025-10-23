using System.Collections;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the Pokémon battle intro sequence.
    /// Waits for the intro animation to finish before moving to the player action state.
    /// </summary>
    public class BattleIntroState : IBattleState
    {
        private bool isSequenceFinished;
        private readonly BattleStateMachine machine;

        /// <summary>
        /// Cached reference to the active <see cref="BattleView"/> context.
        /// </summary>
        private BattleView View => machine.BattleView;

        /// <summary>
        /// Creates a new <see cref="BattleIntroState"/> instance.
        /// </summary>
        /// <param name="machine">The owning battle state machine.</param>
        public BattleIntroState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when the state becomes active.
        /// Starts the intro sequence coroutine.
        /// </summary>
        public void Enter()
        {
            isSequenceFinished = false;
            View.BattleAnimation.OnSequenceFinish += OnIntroSequenceFinish;
            View.StartCoroutine(PlayIntroSequence());
        }

        /// <summary>
        /// Called when the state is exited.
        /// Unsubscribes from animation events and resets the sequence flag.
        /// </summary>
        public void Exit()
        {
            View.BattleAnimation.OnSequenceFinish -= OnIntroSequenceFinish;
            isSequenceFinished = false;
        }

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        public void Update() { }

        private void OnIntroSequenceFinish()
        {
            isSequenceFinished = true;
        }

        /// <summary>
        /// Plays the intro animation and transitions to the next state upon completion.
        /// </summary>
        private IEnumerator PlayIntroSequence()
        {
            View.BattleAnimation.PlayIntroSequence();
            yield return new WaitUntil(() => isSequenceFinished);
            machine.SetState(new PlayerActionState(machine));
        }
    }
}

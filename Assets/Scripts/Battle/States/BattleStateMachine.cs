using PokemonGame.Utilities;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Manages the battle flow by handling transitions between <see cref="IBattleState"/> implementations.
    /// </summary>
    internal class BattleStateMachine
    {
        /// <summary>
        /// Gets the currently active battle state.
        /// </summary>
        internal IBattleState CurrentState { get; private set; }

        /// <summary>
        /// Gets the view component responsible for rendering the battle UI and animations.
        /// </summary>
        internal BattleView BattleView { get; }

        /// <summary>
        /// Initializes the machine with a reference to the battle's view.
        /// </summary>
        internal BattleStateMachine(BattleView battleView)
        {
            BattleView = battleView;
        }

        /// <summary>
        /// Transitions the battle to a new state. 
        /// Handles the Exit logic of the old state and Enter logic of the new state.
        /// </summary>
        /// <param name="newState">The state to transition into.</param>
        internal void SetState(IBattleState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

             Log.Info(CurrentState?.GetType().Name, $"Transitioning from {CurrentState?.GetType().Name} to {newState.GetType().Name}");

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Processes the per-frame logic for the active state.
        /// </summary>
        internal void Update()
        {
            CurrentState?.Update();
        }
    }
}
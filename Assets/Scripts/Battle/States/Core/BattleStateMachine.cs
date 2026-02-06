namespace PokemonGame.Battle.States.Core
{
    /// <summary>
    /// Manages the battle flow by transitioning between and updating <see cref="IBattleState"/> instances.
    /// </summary>
    internal sealed class BattleStateMachine
    {
        /// <summary>
        /// The currently active battle state.
        /// </summary>
        internal IBattleState CurrentState { get; private set; }

        /// <summary>
        /// The active battle view associated with this state machine.
        /// </summary>
        internal BattleView BattleView { get; }

        /// <summary>
        /// Initializes a new battle state machine bound to a specific battle view.
        /// </summary>
        /// <param name="battleView">
        /// The battle view used to present and control the battle UI.
        /// </param>
        internal BattleStateMachine(BattleView battleView)
        {
            BattleView = battleView;
        }

        /// <summary>
        /// Transitions to a new state, invoking Exit on the current state
        /// and Enter on the new one.
        /// </summary>
        /// <param name="newState">
        /// The state to transition to.
        /// </param>
        internal void SetState(IBattleState newState)
        {
            if (newState == null || newState == CurrentState)
            {
                return;
            }

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Updates the current battle state.
        /// </summary>
        internal void Update()
        {
            CurrentState?.Update();
        }
    }
}

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Controls the flow of battle by managing the active <see cref="IBattleState"/>.
    /// Handles transitions between states such as intro, player actions, and results.
    /// </summary>
    public class BattleStateMachine
    {
        /// <summary>
        /// The currently active battle state.
        /// </summary>
        public IBattleState CurrentState { get; private set; }

        /// <summary>
        /// Reference to the <see cref="BattleView"/> associated with this state machine.
        /// </summary>
        public BattleView BattleView { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleStateMachine"/> class.
        /// </summary>
        /// <param name="battleView">The <see cref="BattleView"/> that this state machine controls.</param>
        public BattleStateMachine(BattleView battleView)
        {
            BattleView = battleView;
        }

        /// <summary>
        /// Switches to a new battle state.
        /// Calls <see cref="IBattleState.Exit"/> on the current state and
        /// <see cref="IBattleState.Enter"/> on the new state.
        /// </summary>
        /// <param name="newState">The new state to transition to.</param>
        public void SetState(IBattleState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Updates the currently active state each frame.
        /// </summary>
        public void Update()
        {
            CurrentState?.Update();
        }
    }
}

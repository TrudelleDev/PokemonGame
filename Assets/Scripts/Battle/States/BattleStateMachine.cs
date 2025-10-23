namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Manages battle state transitions and updates during a Pokémon battle.
    /// </summary>
    public class BattleStateMachine
    {
        /// <summary>
        /// Currently active battle state.
        /// </summary>
        public IBattleState CurrentState { get; private set; }

        /// <summary>
        /// Owning battle view context.
        /// </summary>
        public BattleView BattleView { get; }

        /// <summary>
        /// Initializes a new BattleStateMachine with the given battle view context.
        /// </summary>
        /// <param name="battleView">The parent BattleView that owns this state machine.</param>
        public BattleStateMachine(BattleView battleView)
        {
            BattleView = battleView;
        }

        /// <summary>
        /// Switches to a new battle state.
        /// </summary>
        /// <param name="newState">State to activate.</param>
        public void SetState(IBattleState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Updates the current state each frame.
        /// </summary>
        public void Update()
        {
            CurrentState?.Update();
        }
    }
}

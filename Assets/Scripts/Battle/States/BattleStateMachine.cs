namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Controls the flow of battle by managing the active <see cref="IBattleState"/>.
    /// Handles transitions between states such as Intro, Action Selection, and Results.
    /// </summary>
    public class BattleStateMachine
    {
        /// <summary>
        /// Gets the currently active state governing the battle flow.
        /// </summary>
        public IBattleState CurrentState { get; private set; }

        /// <summary>
        /// Gets the view component responsible for displaying battle information.
        /// </summary>
        internal BattleView BattleView { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleStateMachine"/> class.
        /// </summary>
        /// <param name="battleView">The view component associated with this battle.</param>
        internal BattleStateMachine(BattleView battleView)
        {
            BattleView = battleView;
        }

        /// <summary>
        /// Sets the new active state for the battle, executing the Exit and Enter transitions.
        /// </summary>
        /// <param name="newState">The state to transition to.</param>
        public void SetState(IBattleState newState)
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
        /// Executes the Update logic for the currently active state.
        /// </summary>
        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
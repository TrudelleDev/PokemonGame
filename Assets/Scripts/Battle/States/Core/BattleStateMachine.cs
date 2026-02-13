namespace MonsterTamer.Battle.States.Core
{
    /// <summary>
    /// Manages the battle flow by transitioning between and updating <see cref="IBattleState"/> instances.
    /// </summary>
    internal sealed class BattleStateMachine
    {
        internal IBattleState CurrentState { get; private set; }
        internal BattleView BattleView { get; }

        internal BattleStateMachine(BattleView battleView) => BattleView = battleView;

        /// <summary>
        /// Transitions to the next state, ensuring the previous state is exited correctly.
        /// </summary>
        internal void SetState(IBattleState nextState)
        {
            if (nextState is null || nextState == CurrentState) return;

            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState?.Enter();
        }

        internal void Update() => CurrentState?.Update();
    }
}
namespace MonsterTamer.Battle.States.Core
{
    /// <summary>
    /// Defines the lifecycle contract for a battle state.
    /// </summary>
    internal interface IBattleState
    {
        /// <summary>
        /// Called when the state becomes active.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        void Update();

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        void Exit();
    }
}

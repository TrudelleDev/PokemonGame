namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Defines the base interface for all battle states.
    /// Each state handles its own lifecycle: Enter, Update, and Exit.
    /// </summary>
    internal interface IBattleState
    {
        /// <summary>
        /// Called once when the state becomes active.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        void Update();

        /// <summary>
        /// Called once when the state is about to be replaced or exited.
        /// </summary>
        void Exit();
    }
}

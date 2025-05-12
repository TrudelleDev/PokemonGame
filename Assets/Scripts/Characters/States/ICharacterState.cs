namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Interface for defining character states.
    /// Each state should handle initialization, updating, and cleanup through the Enter, Update, and Exit methods.
    /// </summary>
    public interface ICharacterState
    {
        /// <summary>
        /// Called when entering the state. Initializes state-specific behavior.
        /// </summary>
        public void Enter();

        /// <summary>
        /// Called every frame while the state is active. Updates state behavior.
        /// </summary>
        public void Update();

        /// <summary>
        /// Called when exiting the state. Cleans up or transitions to another state.
        /// </summary>
        public void Exit();
    }
}

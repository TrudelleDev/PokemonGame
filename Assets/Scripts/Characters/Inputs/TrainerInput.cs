using PokemonGame.Characters.Direction;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Provides externally forced movement input for trainers (vision, scripts, cutscenes).
    /// </summary>
    internal sealed class TrainerInput : CharacterInput
    {
        public InputDirection ForcedDirection { get; set; } = InputDirection.None;

        protected override void ReadInput()
        {
            // The trainer only moves if we've assigned a ForcedDirection
            CurrentDirection = ForcedDirection;
        }
    }
}
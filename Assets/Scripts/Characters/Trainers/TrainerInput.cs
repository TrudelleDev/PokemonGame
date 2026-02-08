using MonsterTamer.Characters.Directions;

namespace MonsterTamer.Characters.Trainers
{
    /// <summary>
    /// Provides externally forced movement input for trainers (vision, scripts, cutscenes).
    /// </summary>
    public sealed class TrainerInput : CharacterInput
    {
        public InputDirection ForcedDirection { get; set; } = InputDirection.None;

        protected override void ReadInput()
        {
            // The trainer only moves if we've assigned a ForcedDirection
            CurrentDirection = ForcedDirection;
        }
    }
}
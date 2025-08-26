namespace PokemonGame.Characters.Inputs.Enums
{
    /// <summary>
    /// Represents raw directional input from the player or AI.
    /// Includes a neutral state (<see cref="None"/>) when no input is pressed.
    /// </summary>
    public enum InputDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}

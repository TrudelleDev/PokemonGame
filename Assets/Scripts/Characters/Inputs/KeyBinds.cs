using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Defines global key bindings for player input.
    /// </summary>
    internal static class KeyBinds
    {
        // --- Movement ---

        /// <summary>
        /// Key used to move the player upward.
        /// </summary>
        public static readonly KeyCode Up = KeyCode.W;

        /// <summary>
        /// Key used to move the player downward.
        /// </summary>
        public static readonly KeyCode Down = KeyCode.S;

        /// <summary>
        /// Key used to move the player left.
        /// </summary>
        public static readonly KeyCode Left = KeyCode.A;

        /// <summary>
        /// Key used to move the player right.
        /// </summary>
        public static readonly KeyCode Right = KeyCode.D;

        // --- Actions ---

        /// <summary>
        /// Key used to interact with NPCs or objects.
        /// </summary>
        public static readonly KeyCode Interact = KeyCode.Z;

        /// <summary>
        /// Key used to cancel, close menus, or go back.
        /// </summary>
        public static readonly KeyCode Cancel = KeyCode.X;

        /// <summary>
        /// Key used to open the game menu.
        /// </summary>
        public static readonly KeyCode Menu = KeyCode.Return;
    }
}

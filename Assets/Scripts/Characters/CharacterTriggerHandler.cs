using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Raycasting;
using UnityEngine;

namespace MonsterTamer.Characters
{
    /// <summary>
    /// Detects and activates triggers in front of the character,
    /// such as warp tiles, cutscenes, or scripted events.
    /// </summary>
    internal sealed class CharacterTriggerHandler
    {
        private readonly Character character;
        private readonly RaycastSettings raycastSettings;
        private readonly Transform transform;

        /// <summary>
        /// Initializes a new instance of <see cref="CharacterTriggerHandler"/>.
        /// Sets up the character reference and raycast settings for trigger detection.
        /// </summary>
        /// <param name="character">The character that will activate triggers.</param>
        /// <param name="raycastSettings">Configuration for raycasting to detect triggers.</param>
        public CharacterTriggerHandler(Character character, RaycastSettings raycastSettings)
        {
            this.character = character;
            this.raycastSettings = raycastSettings;
            this.transform = character.transform;
        }

        /// <summary>
        /// Executes all triggers in the specified direction.
        /// </summary>
        /// <param name="direction">Direction to check.</param>
        /// <returns>True if at least one trigger was activated.</returns>
        public bool TryTrigger(Vector2 direction)
        {
            bool triggered = false;

            RaycastUtility.RaycastAndCall<ITriggerable>(direction, raycastSettings, transform, triggerable =>
            {
                triggerable.Trigger(character);
                triggered = true;
                return false; // continue checking other triggers
            });

            return triggered;
        }
    }
}

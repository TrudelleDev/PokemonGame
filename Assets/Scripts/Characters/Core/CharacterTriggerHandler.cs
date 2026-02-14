using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Raycasting;
using UnityEngine;

namespace MonsterTamer.Characters.Core
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

        internal CharacterTriggerHandler(Character character, RaycastSettings raycastSettings) =>
            (this.character, this.raycastSettings, this.transform) = (character, raycastSettings, character.transform);

        /// <summary>
        /// Executes all triggers in the specified direction.
        /// </summary>
        /// <param name="direction">Direction to check.</param>
        internal bool TryTrigger(Vector2 direction)
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

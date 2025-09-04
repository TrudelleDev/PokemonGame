using PokemonGame.Characters.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Detects and activates triggers in front of a character,
    /// such as warp tiles, cutscenes, or scripted events.
    /// </summary>
    [DisallowMultipleComponent]
    public class CharacterTriggerHandler : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Raycast configuration that defines how far ahead to check for triggers.")]
        private RaycastSettings raycastSettings;

        private Character character;

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        /// <summary>
        /// Executes all triggers found in the given direction.
        /// Returns true if at least one trigger was activated.
        /// </summary>
        /// <param name="direction">World-space direction to check from the character's position.</param>
        public bool CheckForTriggers(Vector2 direction)
        {
            bool triggered = false;

            RaycastUtility.RaycastAndCall<ITriggerable>(direction, raycastSettings, transform, trigger =>
            {
                trigger.Trigger(character);
                triggered = true;
                return false; // continue checking other triggers
            });

            return triggered;
        }
    }
}

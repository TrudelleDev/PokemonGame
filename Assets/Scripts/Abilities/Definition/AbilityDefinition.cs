using PokemonGame.Abilities.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Abilities.Definition
{
    /// <summary>
    /// Defines a Pokémon ability used to describe its name, description, and unique identifier.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAbilityDefinition", menuName = "ScriptableObjects/Ability Definition")]
    public class AbilityDefinition : ScriptableObject
    {
        // ---- Identity ----

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this ability.")]
        [SerializeField, Required]
        private AbilityID id;

        [BoxGroup("Identity")]
        [Tooltip("Name of the ability as shown in the UI.")]
        [SerializeField, Required]
        private string displayName;

        // ---- Description ----

        [BoxGroup("Description")]
        [Tooltip("Text description of the ability's effect, shown in the UI.")]
        [SerializeField, Required, TextArea(3, 8)]
        private string description;

        // ---- Properties ----

        /// <summary>
        /// Defines the unique identifier for this ability.
        /// </summary>
        public AbilityID ID => id;

        /// <summary>
        /// Defines the display name shown in UI.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Defines the description shown to the player.
        /// </summary>
        public string Description => description;
    }
}

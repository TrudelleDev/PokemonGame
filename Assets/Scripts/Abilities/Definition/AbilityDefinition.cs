using PokemonGame.Abilities.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Abilities.Definition
{
    /// <summary>
    /// Defines a Pokémon ability, including its display name, description, and unique identifier.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAbilityDefinition", menuName = "ScriptableObjects/Ability Definition")]
    public class AbilityDefinition : ScriptableObject
    {
        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this ability.")]
        [SerializeField, Required]
        private AbilityID id;

        [BoxGroup("Identity")]
        [Tooltip("Name shown in UI.")]
        [SerializeField, Required]
        private string displayName;

        [BoxGroup("Description")]
        [Tooltip("Description of what the ability does.")]
        [SerializeField, TextArea(3, 8), Required]
        private string description;

        /// <summary>
        /// Unique identifier used to reference this ability in code.
        /// </summary>
        public AbilityID ID => id;

        /// <summary>
        /// Name of the ability shown in UI.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Text description of the ability's effect, shown in UI.
        /// </summary>
        public string Description => description;
    }
}

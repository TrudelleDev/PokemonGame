using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities
{
    /// <summary>
    /// Defines a Pokémon ability.
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
        /// Unique ID used to reference this ability.
        /// </summary>
        public AbilityID ID => id;

        /// <summary>
        /// Name displayed in the UI.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Description shown in the UI.
        /// </summary>
        public string Description => description;
    }
}

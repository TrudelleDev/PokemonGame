using PokemonGame.Natures.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Natures
{
    /// <summary>
    /// Defines a Pokémon nature, including its display name and unique identifier.
    /// </summary>
    [CreateAssetMenu(fileName = "NewNatureDefinition", menuName = "Pokemon/Natures/Nature Definition")]
    public class NatureDefinition : ScriptableObject
    {
        // ---- Identity ----

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this nature.")]
        [SerializeField, Required]
        private NatureId natureID;

        [BoxGroup("Identity")]
        [Tooltip("Name shown in the UI.")]
        [SerializeField, Required]
        private string displayName;

        // ---- Properties ----

        /// <summary>
        /// Unique identifier used to reference this nature in code.
        /// </summary>
        public NatureId ID => natureID;

        /// <summary>
        /// Display name of the nature shown in UI.
        /// </summary>
        public string DisplayName => displayName;
    }
}

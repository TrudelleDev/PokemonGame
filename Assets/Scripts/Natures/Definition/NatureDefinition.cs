using PokemonGame.Natures.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Natures
{
    /// <summary>
    /// Defines a Pokémon nature, including its display name and unique identifier.
    /// </summary>
    [CreateAssetMenu(fileName = "NewNatureDefinition", menuName = "ScriptableObjects/Nature Definition")]
    public class NatureDefinition : ScriptableObject
    {
        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this nature.")]
        [SerializeField, Required]
        private NatureID natureID;

        [BoxGroup("Identity")]
        [Tooltip("Name shown in the UI.")]
        [SerializeField, Required]
        private string displayName;

        /// <summary>
        /// Unique identifier used to reference this nature in code.
        /// </summary>
        public NatureID ID => natureID;

        /// <summary>
        /// Name of the nature shown in UI.
        /// </summary>
        public string DisplayName => displayName;
    }
}

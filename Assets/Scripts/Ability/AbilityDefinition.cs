using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Ability
{
    /// <summary>
    /// Defines a Pokémon ability used to describe its name, description, and unique identifier.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Ability/Ability Definition")]
    public class AbilityDefinition : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Name of the ability as shown in the UI.")]
        private string displayName;

        [SerializeField, Required, TextArea(3, 8)]
        [Tooltip("Text description of the ability's effect, shown in the UI.")]    
        private string description;

        public string DisplayName => displayName;
        public string Description => description;
    }
}

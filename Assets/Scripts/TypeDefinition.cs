using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame
{
    /// <summary>
    /// Defines a type used by a Pokémon or move, including its icon.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTypeDefinition", menuName = "Types/Type Definition")]
    public class TypeDefinition : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Icon representing this type in the UI.")]       
        private Sprite sprite;

        /// <summary>
        /// Icon representing the type in the UI.
        /// </summary>
        public Sprite Sprite => sprite;
    }
}

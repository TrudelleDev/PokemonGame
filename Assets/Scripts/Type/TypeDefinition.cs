using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Type
{
    /// <summary>
    /// Define a Pokémon or move type, including its icon and type effectiveness.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Type/Definition", fileName = "NewTypeDefinition")]
    public class TypeDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Icon representing this type in the UI.")]
        private Sprite icon;

        [SerializeField, Tooltip("The effectiveness groups defining how this type interacts with other types.")]
        private TypeEffectivenessGroups effectivenessGroups;

        public Sprite Icon => icon;
        public TypeEffectivenessGroups EffectivenessGroups => effectivenessGroups;
    }
}

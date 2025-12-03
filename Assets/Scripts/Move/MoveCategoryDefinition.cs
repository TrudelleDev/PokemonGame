using PokemonGame.Move.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move
{
    /// <summary>
    /// Defines a Pokémon move's damage category (Physical, Special, or Status),
    /// including the icon used in UI representations.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Move/Categories/Move Category Definition")]
    public class MoveCategoryDefinition : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Icon representing the move's damage category.")]
        private Sprite icon;

        [SerializeField]
        [Tooltip("The damage category type (Physical, Special, or Status).")]
        private MoveCategory category;

        public Sprite Icon => icon;
        public MoveCategory Category => category;
    }
}

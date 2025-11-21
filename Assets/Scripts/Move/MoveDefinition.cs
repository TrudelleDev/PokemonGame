using PokemonGame.Move.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move
{
    /// <summary>
    /// Defines a Pokemon move, including stats, classification, and in-game effect text.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Move/Move Definition")]
    public class MoveDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Display name of the move.")]
        private string displayName;

        [SerializeField, Tooltip("Base stats of the move: power, accuracy, and PP.")]
        private MoveInfo moveInfo;

        [SerializeField, Tooltip("Classification of the move: type and category.")]
        private MoveClassification classification;

        [SerializeField, Required, TextArea(5, 10)]
        [Tooltip("Description or effect text shown to the player.")]
        private string effect;

        public string DisplayName => displayName;
        public MoveInfo MoveInfo => moveInfo;
        public MoveClassification Classification => classification;
        public string Effect => effect;
    }
}
